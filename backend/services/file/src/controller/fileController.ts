import { Request, Response, NextFunction } from 'express';
import logging from '../config/logging';
import { FileService } from '../services/fileService';

export class FileController {

    private fileService: FileService = new FileService();

    public getFile(req: Request, res: Response, next: NextFunction) {
        logging.info('getFile :: Fetching file');
        this.fileService.getFile(req, res, next);
    }

    public getFiles(req: Request, res: Response, next: NextFunction) {
        logging.info('getFiles :: Fetching all files');
        this.fileService.getFiles(req, res, next);
    }

    public uploadFile(req: Request, res: Response, next: NextFunction) {
        logging.info('uploadFile :: Uploading file');
        this.fileService.uploadFile(req, res, next);
    }

    public deleteFile(req: Request, res: Response, next: NextFunction) {
        logging.info('deleteFile :: Deleting file');
        this.fileService.deleteFile(req, res, next);
    }
}
