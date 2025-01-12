import express, { Request, Response, NextFunction } from "express";

export class ApiRoutes {
  public routes(app: express.Application): void {
    app.get(
      "/fileapi/healthcheck",
      (req: Request, res: Response, next: NextFunction) => {
        return res.status(200).json({ message: "I am up and running.!" });
      }
    );
    // Define other API routes here
    app.get('/favicon.ico', (req: Request, res: Response, next: NextFunction) =>
      res.status(204).end()
    );
  }
}
