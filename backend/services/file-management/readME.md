
## Initialize a new Node.js project

To initialize a new Node.js project, run the following command in your project directory:

```
npm init -yes
```

## Setup the VSCODE (Optional step)
This is optional step to setup the vscode
1) create a folder .vscode folder 
2) after that create a settings.json file

```json
{
    "editor.defaultFormatter": "esbenp.prettier-vscode",
    "editor.bracketPairColorization.enabled": true,
    "editor.formatOnSave": true,
    "editor.formatOnPaste": true,
}
```

## Install the necessary dependencies

To install the necessary dependencies, run the following command in your project directory:

```
npm install -g typescript ts-node nodemon
npm install express typescript ts-node dotenv
npm install @types/express @types/node typescript --save-dev
```

## Create a tsconfig.json file or run the below common to genrate the tsconfig.json

```
npm install -g tsc
tsc --init
```

```json
{
    "compilerOptions": {
        "target": "es6",
        "module": "commonjs",
        "outDir": "dist",
        "strict": true,
        "esModuleInterop": true
    },
    "include": [
        "src/**/*.ts"
    ],
    "exclude": [
        "node_modules"
    ]
}
```

## Create a src directory

Create a `src` directory in the root of your project and create a new TypeScript file, e.g., `server.ts`, inside the `src` directory.

## create config folder inside src

## Define your API routes

In `server.ts`, import the necessary modules and define your API routes. Here's an example:

```typescript
import express from 'express';

const app = express();
const port = 3000;

app.get('/', (req, res) => {
    res.send('Hello, World!');
});

app.listen(port, () => {
    console.log(`Server is running on port ${port}`);
});
```

## Add scripts to package.json

Open your `package.json` file and add the following scripts:

```json
"scripts": {
    "start": "ts-node src/index.ts",
    "build": "tsc"
}
```

## Start your API in development mode

To start your API in development mode, run the following command in your project directory:

```
npm start
```

Your API will be accessible at `http://localhost:5000`.

## Build your TypeScript code

To build your TypeScript code into JavaScript, run the following command in your project directory:

```
npm run build
```

The compiled JavaScript files will be placed in the `dist` directory.

That's it! You have now created a TypeScript Node.js API. Feel free to add more routes and functionality as needed.



Running Commands
Run in Development Mode:

npm run start:dev
Run in QA Mode:

npm run start:qa
Run in Production Mode:

npm run start:prod
Build Docker Image:

npm run docker:build
Run Docker Container in Development Mode:

npm run docker:run:dev
Run Docker Container in QA Mode:

npm run docker:run:qa
Run Docker Container in Production Mode:

npm run docker:run:prod