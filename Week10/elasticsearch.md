# Elasticsearch
[Elasticsearch](http://www.elasticsearch.org/) is a search server based on
[Lucene](http://lucene.apache.org/).  It provides a distributed,
multitenant-capable full-text search engine with a RESTful web interface and
schema-free JSON documents.

A great tutorial for beginners in Elasticsearch can be found at [http://okfnlabs.org/blog/2013/07/01/elasticsearch-query-tutorial.html](http://okfnlabs.org/blog/2013/07/01/elasticsearch-query-tutorial.html).

A common use case for engines like Elasticsearch is to replicate data that we
have in our persistent storage and use the search engine for searching for data
across multiple document types. Elasticsearch includes a blazingly fast text
search feature and has it's own DSL for crafting ad-hoc queries.

Databases are good for storing data, but they are not as good when it comes to
searching.  In those cases it is worth it investigating tools such as
Elasticsearch.


## Download and install
To setup an Elasticsearch node, download the latest version from.
[http://www.elasticsearch.org/overview/elkdownloads/](http://www.elasticsearch.org/overview/elkdownloads/)

Download a compression that you like, uncompress it, and place it somewhere on
your disk.

    % unzip elasticsearch-1.3.4.zip
    cd elasticsearch-1.3.4

Elasticsearch comes with sensible defaults and a nice startup script to start.
To start a single node execute a script with the name `elasticsearch` under the
`bin` folder.

    bin/elasticsearch

Elasticsearch is highly clusterable and by default your node might try to join
another cluster on the same network. You should alter your cluster name so that
your node will be your own cluster.

You can control the name of your cluster in `conf/elasticsearch.yaml`

    cluster.name: some-name

When you start an Elasticsearch node you should see an output like the
following.

    % elasticsearch-1.3.4  bin/elasticsearch
    [2014-10-20 09:18:00,242][INFO ][node                     ] [Perun] version[1.3.4], pid[4535], build[a70f3cc/2014-09-30T09:07:17Z]
    [2014-10-20 09:18:00,242][INFO ][node                     ] [Perun] initializing ...
    [2014-10-20 09:18:00,247][INFO ][plugins                  ] [Perun] loaded [], sites []
    [2014-10-20 09:18:02,907][INFO ][node                     ] [Perun] initialized
    [2014-10-20 09:18:02,908][INFO ][node                     ] [Perun] starting ...
    [2014-10-20 09:18:03,020][INFO ][transport                ] [Perun] bound_address {inet[/0:0:0:0:0:0:0:0:9300]}, publish_address {inet[/192.168.1.4:9300]}
    [2014-10-20 09:18:03,039][INFO ][discovery                ] [Perun] hscluster/7icMi0pZQoaU-yor_eQfzQ
    [2014-10-20 09:18:06,057][INFO ][cluster.service          ] [Perun] new_master [Perun][7icMi0pZQoaU-yor_eQfzQ][localhost.localdomain][inet[/192.168.1.4:9300]], reason: zen-disco-join (elected_as_master)
    [2014-10-20 09:18:06,097][INFO ][http                     ] [Perun] bound_address {inet[/0:0:0:0:0:0:0:0:9200]}, publish_address {inet[/192.168.1.4:9200]}
    [2014-10-20 09:18:06,097][INFO ][node                     ] [Perun] started
    [2014-10-20 09:18:06,124][INFO ][gateway                  ] [Perun] recovered [0] indices into cluster_state

From this output we see that the cluster name is hscluster. The node got the
name Perun. If you don't specify the name of your node Elasticsearch will
provide one for you. This node was elected the master node for our cluster and
is in the state of accepting other nodes into the cluster for replication.
Finally we can see that the Elasticsearch API has the public address
localhost:9200. 

Remember to give your cluster a name. If you use the default one, your node
might join a cluster that is on the same network, specially if your are doing
this in a classroom where other people are also messing around with
Elasticsearch.


## Indexing
Now let's play around with Elasticsearch by adding, updating and deleting
documents.

To add documents to Elasticsearch we use the Elasticsearch API and we use the
well known HTTP methods to do so. 

To index a document we do a PUT command with a JSON object in the body.
The url that we PUT to is on the following format.

    http://localhost:9200/index/type/id

You must provide an index and a type for the document, but the id is optional.
The index parameter is the name of the index where the document should live
under. You can think about the index is the database. If a given index does not
exists when you put into it, Elasticsearch will automatically create one for
you that you can use.

The type parameter is for you to sort out document types. In terms of
databases, you can think about the type as a table, or a collection.

If you don't provide an id, Elasticsearch will provide one for you. But if you
decided to skip the id, you should do a POST instead of PUT.

Throughout this document let us assume that we are building a massive blog
system and we wish to store our blog entries in search index that users can use
to search for.

To begin with we will manually add the post using `curl`, but later in this
document we will create a event hook on SQLAlchemy and Mongoose that handles
the communications with Elasticsearch for us.


Now let's index a document.


    % curl -XPUT http://localhost:9200/entries/entry/1 -d '{  
    "title": "Today I learned to search",
    "data": "This is my blog entry content",
    "created": "2014-12-24T14:24:23"}'
    {"_index":"entries","_type":"entry","_id":"1","_version":1,"created":true}%


Here we add a single document to the index entries in type entry with the id 1
and the content is a JSON object with my blog entry data. We can see in the
response that we get from the Elasticsearch API that this was successful and
the document was created.  We can also see that this document has the version
number one. You can use this counter to see how often a given document has been
updated.  Internally, Elasticsearch relies on this field when replication
documents between nodes.

You can fetch the document back by the document id as follows.

    % curl http://localhost:9200/entries/entry/1
    {"_index":"entries","_type":"entry","_id":"1","_version":1,"found":true,"_source":{  
        "title": "Today I learned to search",
        "data": "This is my blog entry content",
        "created": "2014-12-24T14:24:23"}}%

We get a JSON object back and in the `_source` property we can find the document that
we previously indexed.

If we `PUT` the document again, using the same id, the document is updated in the index.

    curl -XPUT http://localhost:9200/entries/entry/1 -d '{
    "title": "Today I learned to search",
    "data": "Lets change the blog content", 
    "created": "2014-12-24T14:24:23"}'
    {"_index":"entries","_type":"entry","_id":"1","_version":2,"created":false}%   

    % curl http://localhost:9200/entries/entry/1            
    {"_index":"entries","_type":"entry","_id":"1","_version":2,"found":true,"_source":{
    "title": "Today I learned to search",
    "data": "Lets change the blog content",
    "created": "2014-12-24T14:24:23"}}%

As you can see, the version has been incremented by one and when we fetch the
document again the JSON object has been updated.

You can remove a given document from the index using the `DELETE` HTTP method.

    % curl -XDELETE http://localhost:9200/entries/entry/1
    {"found":true,"_index":"entries","_type":"entry","_id":"1","_version":3}%
    
    % curl http://localhost:9200/entries/entry/1         
    {"_index":"entries","_type":"entry","_id":"1","found":false}%

If we try to fetch the document after the delete, we can see that it has been
removed.

You can also delete all the documents under a given type as follows

    curl -XDELETE http://localhost:9200/entries/entry

or, remove the whole index
    
    curl -XDELETE http://localhost:9200/entries/

# Search
Now we have seen how we can create, update and delete documents. Let us now look at the
real powers that Elasticsearch has to offer, searching.

Before we search, lets add some documents to the index.

    curl -XDELETE http://localhost:9200/entries


    curl -XPUT http://localhost:9200/entries/entry/1 -d '{
      "title": "Everything that you know about C++ is a lie",
      "data": "Bjarne is a Danish bloke and he hates C",
      "created": "2012-01-10T00:12:12",
      "tags": ["programming", "c++"]
    }'

    curl -XPUT http://localhost:9200/entries/entry/2 -d '{
      "title": "Ruby on rails is just a bubble in bathtub",
      "data": "Yeah, Ruby on rails is awesome, but scaffolding is not!",
      "created": "2013-04-02T12:34:02",
      "tags": ["programming", "ruby", "rails"]
    }'

    curl -XPUT http://localhost:9200/entries/entry/3 -d '{
      "title": "Smaladrengirnir go gold",
      "data": "The first album by the band Smaladrengirnir has sold millions of copies and is now a gold album",
      "created": "2013-06-02T15:21:10",
      "tags": ["music", "smalar"]
    }'



In order to search with Elasticsearch we use the _search endpoint. This
endpoint both on index and type level. Thus you can curl on the following links
to fetch all documents

    curl http://localhost:9200/_search
    curl http://localhost:9200/entries/_search
    curl http://localhost:9200/entries/entry/_search

This allows you to perform a search over multiple indices or narrow your search
down to single type.

When we do a curl on the above mentioned links we can see that we find the entries that
we added before in the hits array.

Elasticsearch provides a full Query DSL (Domain Specific Language) based on
JSON to define queries. The Elasticsearch query language is defined here
[http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/query-dsl.html](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/query-dsl.html)

We will go over the basics of the language but we will not showcase all the
possible features of the language. When you are in doubt, or you need to craft
an ad-hoc query then this documentation is the one that you should refer to.


## Basic text search
To perform a query we POST on the _search endpoint with our query in the body of the request.

    curl -XPOST http://localhost:9200/entries/_search -d '
    {
        "query":{
            // query goes here! 
        }
    }'

The query DSL features a long list of different types of queries that we can
use. For "ordinary" free text search we'll most likely want to use one called
"query string query".

Let's create a query and search for entries that contain the work "scaffolding" in them

    curl -XPOST http://localhost:9200/entries/_search -d '
    {
        "query":{
            "query_string": {
                "query": "scaffolding",
                "default_field" : "data"
            } 
        }
    }'

When execute this query,

    curl -XPOST http://localhost:9200/entries/_search -d '
    quote>     {
    quote>         "query":{
    quote>             "query_string": {
    quote>                 "query": "scaffolding",
    quote>                 "default_field" : "data"
    quote>             } 
    quote>         }
    quote>     }'
    {"took":3,"timed_out":false,"_shards":{"total":5,"successful":5,"failed":0},"hits":{"total":1,"max_score":0.095891505,"hits":[{"_index":"entries","_type":"entry","_id":"2","_score":0.095891505,"_source":{
      "title": "Ruby on rails is just a bubble in bathtub",
      "data": "Yeah, Ruby on rails is awesome, but scaffolding is not!",
      "created": "2013-04-02T12:34:02",
      "tags": ["programming", "ruby", "rails"]
    }}]}}%            

We get back one entry where we have the text scaffolding in and that is document with id 2.


## Query in list

    curl -XPOST http://localhost:9200/entries/_search -d '
    {
       "query" : {
          "filtered" : {
             "filter" : {
                "terms" : {
                   "tags" : [
                      "ruby",
                      "smalar"
                   ]
                }
             }
          }
       }
    }
    '

## query by range on dates.
    
    curl -XPOST http://localhost:9200/entries/_search -d '
    {
        "query" : {
            "range" : {
                "created" : {
                    "from" : "2011-06-02T15:21:10",
                    "to" : "2012-06-02T15:21:10"
                }
            }
        }
    }'
