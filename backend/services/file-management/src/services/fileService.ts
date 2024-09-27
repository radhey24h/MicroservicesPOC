import { BlobServiceClient, ContainerClient } from '@azure/storage-blob';
import { Request, Response, NextFunction } from 'express';
import logging from '../config/logging';
import { config } from '../config/config';
import fs from "fs";

export class FileService {
  private blobServiceClient: BlobServiceClient;
  private containerClient: ContainerClient;

  constructor() {
    this.blobServiceClient = BlobServiceClient.fromConnectionString(
      config.storage.connectionString
    );
    this.containerClient = this.blobServiceClient.getContainerClient(
      config.storage.containerName
    );
  }

  public async getFile(req: Request, res: Response, next: NextFunction) {
    try {
      const blobName = req.params.name;
      const blobClient = this.containerClient.getBlobClient(blobName);
      const downloadBlockBlobResponse = await blobClient.download();
      const data = await this.streamToString(
        downloadBlockBlobResponse.readableStreamBody!
      );
      res.status(200).send(data);
    } catch (error) {
      logging.error("getFile", error);
      next(error);
    }
  }

  public async getFiles(req: Request, res: Response, next: NextFunction) {
    try {
      const blobs = [];
      for await (const blob of this.containerClient.listBlobsFlat()) {
        blobs.push(blob.name);
      }
      res.status(200).json(blobs);
    } catch (error) {
      logging.error("getFiles", error);
      next(error);
    }
  }

  public async uploadFile(req: Request, res: Response, next: NextFunction) {
    try {
      const file = req.file; // The uploaded file
      if (!file) {
        res.status(400).send("No file uploaded.");
        return;
      }

      const blobName = file.originalname;
      const blockBlobClient = this.containerClient.getBlockBlobClient(blobName);
      const fileStream = fs.createReadStream(file.path);

      await blockBlobClient.uploadStream(fileStream, file.size);
      res.status(201).send(`File uploaded: ${blobName}`);

      // Clean up the local file after uploading
      fs.unlinkSync(file.path);
    } catch (error) {
      logging.error("uploadFile", error);
      next(error);
    }
  }

  public async deleteFile(req: Request, res: Response, next: NextFunction) {
    try {
      const blobName = req.body.name;
      const blockBlobClient = this.containerClient.getBlockBlobClient(blobName);
      await blockBlobClient.delete();
      res.status(200).send(`File deleted: ${blobName}`);
    } catch (error) {
      logging.error("deleteFile", error);
      next(error);
    }
  }

  private async streamToString(
    readableStream: NodeJS.ReadableStream
  ): Promise<string> {
    return new Promise((resolve, reject) => {
      const chunks: Buffer[] = [];
      readableStream.on("data", (data) => {
        chunks.push(data instanceof Buffer ? data : Buffer.from(data));
      });
      readableStream.on("end", () => {
        resolve(Buffer.concat(chunks).toString("utf-8"));
      });
      readableStream.on("error", reject);
    });
  }
}
