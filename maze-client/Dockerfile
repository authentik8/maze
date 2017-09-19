from node
RUN mkdir /app
WORKDIR /app
ADD package.json /app/package.json
RUN npm install
RUN npm install -g yarn
RUN npm install -g serve 
ENV PORT 80
COPY . /app
EXPOSE  80
RUN npm run-script build
# In your Dockerfile.
# Run serve when the image is run.
CMD serve -s build
