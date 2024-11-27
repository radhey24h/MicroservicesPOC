import * as dotenv from 'dotenv';

// Load environment variables from the appropriate .env file

const envPath = `.env.${process.env.NODE_ENV || 'development'}`;
dotenv.config({ path: envPath });
//dotenv.config();
export const config = {
  environment: process.env.NODE_ENV === 'local',
  server: {
    hostname: process.env.SERVER_HOSTNAME || 'localhost',
    port: process.env.SERVER_PORT ? Number(process.env.SERVER_PORT) : 12345,
  },
  storage: {
    account: process.env.STORAGE_ACCOUNT || '',
    key: process.env.STORAGE_KEY || '',
    containerName: process.env.CONTAINER_NAME || '',
    connectionString: process.env.CONNECTION_STRING || '',
  }
};
