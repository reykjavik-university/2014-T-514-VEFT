var mongoose = require('mongoose'),
    slugify = require('slugify'),
    elasticsearch = require('elasticsearch');

var client = new elasticsearch.Client({
  host: 'localhost:9200'
});


var setTags = function(tags){
  return tags.split(',') 
}

var getTags = function(tags){
  return tags.join(',')
}

var blogSchema = new mongoose.Schema({
  title: {type: String, required: true},
  slug: {type: String, unique: true},
  created: {type: Date, default:Date.now},
  tags: {type: [], set: setTags, get: getTags}
});

blogSchema.pre('save', function(next){
  this.slug = slugify(this.title);
  next();
});

blogSchema.post('save', function(b) {
  client.index({
    index: 'blogs',
    type: 'blog',
    id: String(b._id),
    body: b}, 
    function (error, response) {
      console.log(error);
      console.log(response);
  });
});


var Blog = mongoose.model('Blog', blogSchema);

module.exports = {'Blog': Blog};

