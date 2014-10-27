# Lab class Week 11

## Exercise 1
Write a RabbitMQ produces that adds messages to RabbitMQ on regular interval.

Write a Node.js consumer that consumes the messages from that queue and prints tham out to stdout.

There is a nice library for Node.js named amqp

    npm install amqp --save
    
That can be used as follows.

    var amqp = require('amqp');
    
    var connection =
      amqp.createConnection({url: "amqp://guest:guest@localhost:5672"},
                                      {defaultExchangeName: "amq.topic"});
    
    connection.on('ready', function () {
      connection.queue("foobar",{autoDelete:false}, function(queue){
        queue.bind('#'); 
        queue.subscribe(function (message) {
          var encoded_payload = unescape(message.data)
          var payload = JSON.parse(encoded_payload)
          console.log('Recieved a message:')
          console.log(payload)
        })
      })
    })

The aim of this execise is for you to install RabbitMQ on your machine and produce and consume messages in two different languages.


# Exercise 2
Write an API with the following routes

- POST /order
- GET /order/<order_id> 

When you post to order, order id is generated and a message with the order details is added to queue named "incoming_orders". When the message has been added to the queue, the order_id is written to the response.

You can do a get on `/order/<orderid>` to see the status of the order. Implement two statuses, "not-done" or "done".

You can store the order state in a sqlite database using SQLAlchemy or in Memcached.

Then write a consumer in Python that listens to the queue, picks up the message, sleeps for 4 seconds and updates the order to a done-state.

This is similar to our pizza example written in class.


