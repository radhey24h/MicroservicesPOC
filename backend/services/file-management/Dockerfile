# Use an official Node.js runtime as a parent image
FROM node:18-alpine

# Set the working directory
WORKDIR /usr/src/app

# Copy package.json and package-lock.json
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application code
COPY . .

# Build the TypeScript code
RUN npm run build

# Expose the port the app runs on
EXPOSE 3000

# Define environment variable
ARG NODE_ENV=production
ENV NODE_ENV=${NODE_ENV}

# Start the app
CMD ["node", "dist/server.js"]

# docker build --build-arg NODE_ENV=development -t my-api:dev .
# docker build --build-arg NODE_ENV=qa -t my-api:qa .
# docker build --build-arg NODE_ENV=production -t my-api:prod .
# docker run -p 3000:3000 -e DB_HOST=localhost my-api:dev  # Development environment
# docker run -p 3000:3000 -e DB_HOST=qa-db.example.com my-api:qa  # QA environment
# docker run -p 3000:3000 -e DB_HOST=prod-db.example.com my-api:prod  # Production environment