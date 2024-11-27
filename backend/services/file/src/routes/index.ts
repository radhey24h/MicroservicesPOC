import express from 'express';
import { ApiRoutes } from './apiRoutes';
import { FileRoutes } from './fileRoutes';

export class AppRouter {
    public apiRoute: ApiRoutes = new ApiRoutes();
    public fileRoutes: FileRoutes = new FileRoutes();

    public routes(app: express.Application): void {
        this.apiRoute.routes(app);
        this.fileRoutes.routes(app);
    }
}
