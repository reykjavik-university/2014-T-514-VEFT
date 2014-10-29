import pika as p
import json
import time
connection = p.BlockingConnection(p.ConnectionParameters('localhost'))
channel = connection.channel()
channel.queue_declare('pizza')


def callback(ch, method, properties, body):
    """
    Callback function when we get a message from RabbitMQ
    This message wil handle our logic.
    """
    print 'We got a pizza order'
    data = json.loads(body)
    pizza_id = data.get('pizza_id')

    time.sleep(20)
    fs = open('/tmp/ready-pizza.txt', 'aw+')
    fs.write(pizza_id+'\n')
    fs.close()

    print 'THE PIZZA IS READY'

channel.basic_consume(callback, queue='pizza', no_ack=True)
channel.start_consuming()
