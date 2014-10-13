from flask import Flask, request, jsonify, render_template

from utils import get_score, incr_score

app = Flask(__name__)


@app.route('/')
def index():
    return render_template('index.html',
                           left=get_score('left'),
                           right=get_score('right'))


@app.route('/balance/<direction>', methods=['POST', 'GET'])
def direction(direction):
    direction = direction.lower()
    if direction not in ['left', 'right']:
        return 'Unknown direction {0}'.format(direction), 404

    if request.method == 'GET':
        if direction == 'left':
            return jsonify({'left': get_score('left')})
        else:
            return jsonify({'right': get_score('right')})
    else:
        incr_score(str(direction))

    return 'ok'

if __name__ == '__main__':
    app.run(debug=False)
