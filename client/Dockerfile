# Stage 1: Build the app (for both dev and prod)
FROM node:slim AS build

WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .

# Run the build command (for production)
RUN npm run build

# Stage 2: Development Environment
FROM node:slim AS dev

WORKDIR /app

COPY package*.json ./
RUN npm install
RUN npm install @rollup/rollup-linux-x64-gnu --save-optional
COPY . .

# Vite default port
EXPOSE 5173 

CMD ["npm", "run", "dev"]

# Stage 3: Production Environment
FROM node:18-alpine AS prod

WORKDIR /app

COPY --from=build /app/dist /app/dist

# Install http-server globally for serving the static files
RUN npm install -g http-server

EXPOSE 8080

CMD ["http-server", "dist"]
