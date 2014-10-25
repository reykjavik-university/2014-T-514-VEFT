var express = require('express'),
    Blog = require('./models').Blog,
    mongoose = require('mongoose'),
    bodyParser = require('body-parser'),
    elasticsearch = require('elasticsearch');


var client = new elasticsearch.Client({
  host: 'localhost:9200'
});

app = express();

// parse application/x-www-form-urlencoded
app.use(bodyParser.urlencoded({ extended: false }));

// parse application/json
app.use(bodyParser.json());

var connectMongo = function(){ 
  mongoose.connect('mongodb://localhost/test', {keepAlive: 1});
  console.log('Connecting to mongodb');
};

mongoose.connection.on('disconnected', connectMongo);
connectMongo();



app.get('/api/blogs', function(req, res){
  Blog.find({}, function(err, blogs){
    if(err){
      res.status(503).send('Unable to fetch blogs');
    }
    else{
      res.json(blogs);
    }
  });
});

app.get('/api/blog/:slug', function(req, res){
  var slug = req.params.slug;
  Blog.findOne({slug: slug}, function(err, b) {
    if(err){
      res.status(500).send('Try again later');
    }

    else if(!b){
      res.status(404).send('No entry with slug ' + slug);
    }

    else{
      res.json(b);
    }
  });
});

app.post('/api/blogs', function(req, res) {
  var blog = new Blog(req.body);
  blog.save(function(err, b){
    
    if(err){
      console.log(err);
      res.status(503).send(err);
    }
    else{
      res.status(201).json(b);
    }
  });
});

app.post('/api/search', function(req, res){
  var search_string = req.body.search || "";
  console.log(search_string);
  client.search({
    index: "blogs",
    body: {
      query: {
        match:{
          _all: search_string 
        } 
      }
    }
  }, function(err, response){
    console.log(err);
    res.json(response);
  });

});

app.listen(4000, function(){
  console.log('Server is ready');
});
