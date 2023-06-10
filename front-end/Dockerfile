FROM node:14.17.1

WORKDIR /app

COPY . /app

RUN yarn

EXPOSE 3000

ENTRYPOINT ["yarn", "start"]