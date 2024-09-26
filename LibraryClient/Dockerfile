FROM node:latest

WORKDIR /app

COPY package*.json ./

RUN npm install

ARG REACT_APP_API_URI
ENV REACT_APP_API_URI=${REACT_APP_API_URI}
ARG REACT_APP_IDENTITY_API_URI
ENV REACT_APP_IDENTITY_API_URI=${REACT_APP_IDENTITY_API_URI}

COPY . .

RUN apt-get update 
RUN apt-get --yes install curl
EXPOSE 3000
CMD ["npm", "start"]