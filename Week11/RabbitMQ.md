# RabbitMQ

RabbitMQ is an open source message broker which implements the [AMQP protocol](http://en.wikipedia.org/wiki/Advanced_Message_Queuing_Protocol). The
principal idea of RabbitMQ is to accept and forward messages between processes
on the same machine or over a network.

In the terminology of RabbitMQ (and other message brokers) you have a process
that creates a message and sends it to a message queue. We refer to this
process as "The producer" of the message. Then we have another process
monitoring the queue for new messages. We refer to this process as "The
consumers". Note that there can be many producers and consumers working on the
same queue.

This way of process communication is platform independent and programming
language independent. You can have two systems, written in different
programming languages communicating easily.

RabbitMQ, or similar applications, solve many problems that arise in large
applications and are often used as [ESB (Enterprise service bus)](http://en.wikipedia.org/wiki/Enterprise_service_bus) within a large organizations.

The reason why we discuss message queues in this course is that they can help
us move logic out of request scope to be executed asynchronously. Before we
look at RabbitMQ in action, let's look at a scenario where that need might come
up.

Let us assume that you are implementing a simple web API for your newly created
small on-line store. You have a small warehouse where you store your items and
you have some workers that place the ordered items into boxes and ship them to
customers.

You decided to start simple by implementing the two following methods to manage
orders.
    
First, `POST /order` where users can POST a JSON objects on the format

    {'customer_name': 'Helgi Möller', 'products_ids': [2, 4 , 42]}

Where the `customer_name` field represents the name of the customers and
`product_ids` are the ids of the products which this given customer is ordering
(This API is simplified for the sake of the example). 

Second, `GET /order/:order_num` where users can GET by order id to see the
status of the order using the order identifier returned from `POST /order`.

When a client posts a JSON object, in the view we simply write the details to
database and then we write the order id generated for this order to the
response. If the load on the database is relatively low, this request should
take more then couple of ms.

Now let's dream big. Few years later your small business has grown big and you
now have hundred of employers working at your company and you have added some
"crucial" departments into your organization structure, such as sales and
marketing. To make this nightmare even worse, the sales department is using
[SAP](http://www.sap.com/index.html) and marketing is using some other horrible
tool for collecting statistical information on orders. These systems now need to
be notified when an order comes through our API. Also, the staff working in the
warehouse would appreciate if orders would be printed out on a printer in the
warehouse for them to work with.

A naive approach would be to implement this logic into the view directly. That
is, when the request arrives we write it to our database, then we communicate
with SAP and some other applications. When we are done, we add a printer job
(with generated PDF) with the order details.

If we do this in the scope of the request, the issuing client must wait for all
these tasks to finish before we can send back the response to the client. Many
of these things that we are now doing in the request is completely irrelevant for
the request issuer and we are simply wasting his time waiting for the request.

Here is where RabbitMQ comes in. The only thing that matters for the client is
to get the order id from the server when the request ends. We can then change
our API to do the following.

1. We write the order to database
2. We calculate order id that we can send to the client.
3. We create a message and we add it to an order queue.
4. We write the order id to the response to the client.

The keyword in this list is the word queue. With RabbitMQ we can have pool of
worker threads listening for new messages in the queue. When they get a
message, they can execute the code which notifies the above mentioned system
and adds the print job.

Fantastic. A Message based architecture. I love it.

Let's see how that is done through examples. But first, we need to install
RabbitMQ.

# Installing RabbitMQ
RabbitMQ is an application server that you must setup and execute on a server
(or on your laptop). To install RabbitMQ on OS X we can use Homebrew.

    brew install rabbitmq
    rabbitmq-plugins enable rabbitmq_management

The RabbitMQ server scripts are installed into /usr/local/sbin through Homebrew.
This is not automatically added to your path, so you may want to add it, to be able
to use the RabbitMQ commands. This can be accomplished by either adding 
PATH=$PATH:/usr/local/sbin to your .bash_profile or .profile. Or opening your
/etc/path by writing this in terminal:

    sudo vim /etc/paths
    
(or any other text editor of you choosing) and add /usr/local/sbin to the file.

On Ubuntu, or other Linux distributions that use apt.

    sudo apt-get install rabbitmq-server
    sudo rabbitmq-plugins enable rabbitmq_management

If you are using some other operating system then there are great installing
instructions on
[http://www.rabbitmq.com/download.html](http://www.rabbitmq.com/download.html)

After the install, the RabbitMQ server might been started up automatically for
you. To see if RabbitMQ is running, you can use the
[rabbitmqctl](http://www.rabbitmq.com/man/rabbitmqctl.1.man.html) to view the
status of your RabbitMQ.

    sudo rabbitmqctl status
    
If it didn't start automatically, you can start RabbitMQ in foreground with

    sudo rabbitmq-server    
    
When you install the management plugin you can manage RabbitMQ through a
web console. If you are running RabbitMQ on your local machine, the url should
be [http://localhost:55672](http://localhost:15672/). The management plugin does
also include a browser-based UI where you can observe your queues, see how many
are connected etc. To gain access to the UI you type the same path into your 
browser and use 'guest' for both username and password.
    
# Example 1 

Now let's write a simple producer, which creates messages and adds them to
RabbitMQ. On a random interval, between 0 and 10 seconds, it adds a JSON 
message, on the format

    {'customer': 'somename', 'price': 1234}

The customer name is then randomized over a set of three names and the price
value is randomised between 0 and 99. This loop executes for ever, in a
while-true loop. You can stop this loop by simply pressing `ctrl+c`.

To communicate with RabbitMQ we need a library that knows how to speak the AMQP
protocol. In this example we use a Python library named
[Pika](https://pika.readthedocs.org/en/0.9.14/). To use Pika you must install
it into your virtualenv

    pip install pika
    
Now let's write this client. Place the following code into a file named
producer.py

    import time
    import random
    import pika
    import json
    
    customers_mock = ['hlysig', 'daniel', 'bob', 'alice']
    
    while True:
        # Calculating random customer from the customer_mock. We use this
        # customer in the message that we add to RabbitMQ.
        random.shuffle(customers_mock)
        customer = random.choice(customers_mock)
    
        # We calculate a random price from a range from 0 - 99
        random_price = random.choice(range(100))
    
        # We calculate a random sleep value. The main thread will sleep
        # on for each message sent before next message is sent. This
        # is done in a while-true loop.
        random_sleep = random.choice(range(5))
    
        # We construct a dictionary that holds the message details. This is
        # the message that we add to the RabbitMQ queue.
        message = {'customer': customer, 'price': random_price}
    
        # We create a connection to RabbitMQ using the AMQP Pika library.
        # We connect to a server running on localhost.
        connection = pika.BlockingConnection(
            pika.ConnectionParameters('localhost'))
        channel = connection.channel()
    
        # Before we senda a message to RabbitMQ we verify that the queue exits.
        # This call is idempodent. The first time this function is called the
        # quque is created. Otherwise, the queue is reused.
        channel.queue_declare('orders')
    
        # Here we publish the message to RabbitMQ.
        # Note that the routing key is the name of the queue that we are
        # adding the message to.
        # We serialize the message to a json string.
        channel.basic_publish(exchange='',
                              routing_key='orders',
                              body=json.dumps(message))
        # We close the connection to RabbitMQ to flush the stream.
        connection.close()
        print 'message sent to RabbitMQ:', message
        print 'Sleeing for {} seconds'.format(random_sleep)
        time.sleep(random_sleep)

There are number of lines that are important in this code that we now discuss.

To connect to a RabbitMQ server we create an instance of a class called
BlockingConnection in Pika. There are many other classes that we can use within
Pika to use this, but this one is simple and serves the purpose of this
example.

    connection = pika.BlockingConnection(pika.ConnectionParameters('localhost'))
    
When this line is executed we have a open TCP connection to our RabbitMQ server
that we can use.

Next we open up a channel,

    channel = connection.channel()

The concept of channels in RabbitMQ is for sharing multiple connection through
a single TCP connection. You can think of a channel as a virtual connection
on top of the TCP connection. This is explained better in this [StackOverflow
answer](http://stackoverflow.com/questions/18418936/rabbitmq-and-relationship-between-channel-and-connection)

Next we check if the queue that we are about to use exists,

    channel.queue_declare('orders')

We must check if the queue exists before we send, or consume from it. If we
send to a queue that does not exist, RabbitMQ will discard our message. This
function is idempotent. If the queue does not exist, it is created, otherwise it
is reused.

Next we send a message to RabbitMQ

    channel.basic_publish(exchange='',
                              routing_key='orders',
                              body=json.dumps(message))
                
The first parameter is the exchange type. If this value is empty, we use a
default exchange. You can read more about RabbitMQ exchanges
[here](https://www.rabbitmq.com/tutorials/tutorial-three-python.html).
The routing parameter states which queue the message should go into and the
body is the actual message.

To be sure that the message has been sent and the socket buffer has been
flushed, we close the connection with

    connection.close()
    

Now let's write a simple consumer that watches for new messages in the orders
queue.

    import pika
    
    connection = pika.BlockingConnection(
        pika.ConnectionParameters('localhost'))
    channel = connection.channel()
    
    channel.queue_declare('orders')
    
    
    def callback(ch, method, properties, body):
        """
        Callback function when we get a message from RabbitMQ
        This message wil handle our logic.
        """
        print body
    
    # Starting consuming messages from queue named orders.
    # This call is blocking so you must exit with ctrl+c
    channel.basic_consume(callback, queue='orders', no_ack=True)
    channel.start_consuming()
    
    
# Message acknowledgment

The question of what happens if a task is killed or dies while still in progress. The code in the examples above removes the message from memory after delivery. In other words, when the worker thread has killed the message and all messages that were pending to be processed, are lost and cannot be retrieved. 

Tasks need to be preserved, so we need to devise a way to move messages to another worker when/if a thread dies

To work with that problem RabbitMQ implements ACK (acknowledgement). That means an acknowledgement packet is sent back from the consumer to tell RabbitMQ that the message has been recieved and processed so it is safe to be deleted.

Acknowledgement works well at RabbitMQ side, if an ack packet isn't recieved from the consumer RabbitMQ takes it as the message wasn't processed and redelivers it. This method is highly effective on preserving a message. RabbitMQ doesn't care if the duration of the connection takes longer than normal, it only acts if the connection with the consumer dies.

    def callback(ch, method, properties, body):
        print " [x] Received %r" % (body,)
        time.sleep( body.count('.') )
        print " [x] Done"
        ch.basic_ack(delivery_tag = method.delivery_tag)
    
    channel.basic_consume(callback,
                          queue='hello')

this simple code makes sure that a message is not lost on worker being killed, unacknowledged messages are redelivered


# Message durability

If RabbitMQ server goes down it will lose all of its queues and therefore all of our messages.   To prevent this from happening even though the server crashes we need to mark both the queue and the message as durable.  
Here is how we declare the queue as durable:

       channel.queue_declare(queue='orders', durable=True)

Notice that it is not possible to change a queue to a durable once after it has been declared. 

Now we need to mark our messages as persistent. 

       channel.basic_publish(exchange='',
                      routing_key="orders",
                      body=json.dumps(message),
                      properties=pika.BasicProperties(
                         delivery_mode = 2, # make message persistent
                      ))

When messages and queue is durable RabbitMQ will save all messages to a disk.  Even though all messages are saved it does not guarantee that no messages will be lost.  There is still a short window when RabbitMQ has received the message and has not yet saved it to a disk, and sometimes RabbitMQ stores the message in a cache.  To get a stronger guarantee it is possible to use: publisher confirms.  

                          routing_key='durable_orders',
                          body=json.dumps(message),
                          properties=pika.BasicProperties(
                            delivery_mode = 2,
                          ))
                          
More on this at:
https://www.rabbitmq.com/tutorials/tutorial-two-python.html

