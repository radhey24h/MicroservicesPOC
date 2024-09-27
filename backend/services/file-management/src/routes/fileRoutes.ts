import express = require('express');
import { FileController } from '../controller/fileController';
import multer from "multer";

const upload = multer({ dest: "uploads/" });
export class FileRoutes {

    fileController: FileController = new FileController();

    public routes(app: express.Application): void {
        app.route('/api/getFile/:name').get(this.fileController.getFile.bind(this.fileController));
        app.route('/api/getFiles').get(this.fileController.getFiles.bind(this.fileController));
        app.route('/api/uploadFile').put(upload.single('file'), this.fileController.uploadFile.bind(this.fileController));
        app.route('/api/deleteFile').delete(this.fileController.deleteFile.bind(this.fileController));
    }
}