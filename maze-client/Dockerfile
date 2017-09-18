from node
RUN mkdir /src
WORKDIR /src
ADD package.json /src/package.json
ENV PORT 80
RUN npm install
EXPOSE  80
CMD [ "npm", "start" ]
