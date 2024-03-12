import json
from flask import Flask, request, jsonify

app = Flask(__name__)

@app.route('/api', methods=['POST'])
def api():
    data = request.get_json()
    print("data json: ",data)

    message_content = data['message']
    print("Message content: ", message_content)

    result = {"message": "Hello from Python backend!"}
    return jsonify(result)

if __name__ == '__main__':
    app.run(debug=True)