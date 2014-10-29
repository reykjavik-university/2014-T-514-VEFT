from flask import Flask, request, Response
import json
import pika as p
import uuid

app = Flask(__name__)


def add_message_to_queue(message):
    """
    Function for adding a pizza order to a queue.
    """
    connection = p.BlockingConnection(p.ConnectionParameters('localhost'))
    channel = connection.channel()
    channel.queue_declare('pizza')

    channel.basic_publish(exchange='',
                          routing_key='pizza',
                          body=message)

    connection.close()


@app.route('/order', methods=['POST'])
def order():
    pizza_id = uuid.uuid4().hex
    data = json.loads(request.data)
    pizza_name = data.get('name')
    pizza_size = data.get('size')

    message = json.dumps({'pizza_id': pizza_id,
                          'pizza_name': pizza_name,
                          'pizza_size': pizza_size})

    add_message_to_queue(message)

    return pizza_id


@app.route('/order/<order_id>')
def check_order(order_id):
    for line in open('/tmp/ready-pizza.txt'):
        line = line.strip()
        if line == order_id:
            return 'Pizza is ready'
    return 'not ready'

if __name__ == '__main__':
    app.run(debug=True)
