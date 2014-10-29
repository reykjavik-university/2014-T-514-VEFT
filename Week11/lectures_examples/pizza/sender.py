import pika as p
import random
import json
import time

customer_mock = ['hlysig', 'Dabs', 'Bob', 'Alice']

while True:
    random.shuffle(customer_mock)
    customer = random.choice(customer_mock)
    random_price = random.choice(range(100))
    random_sleep = random.choice(range(1, 6))

    message = {'customer': customer, 'price': random_price}

    connection = p.BlockingConnection(p.ConnectionParameters('localhost'))
    channel = connection.channel()
    channel.queue_declare('order')

    channel.basic_publish(exchange='',
                          routing_key='order',
                          body=json.dumps(message))

    connection.close()

    print 'Sending message', message, 'sleeping for', random_sleep
    time.sleep(random_sleep)
