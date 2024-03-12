from ToGpt import togpt
import json
from flask import Flask, request, jsonify

app = Flask(__name__)

@app.route('/api', methods=['POST'])
def api():
    data = request.get_json()
    message_content = data['message']
    message_object=data['msg_object']
    message_motion=data['msg_motion']
    print("Message content: ", message_content)
    print("Message object: ", message_object)
    print("Message motion: ", message_motion)
    gptresult = togpt(message_content,message_object,message_motion)

    # 解析JSON字符串
    databack = json.loads(gptresult)

    # 提取键值部分
    object_value = databack["object"]
    action_value = databack["action"]

    print("object_value:", object_value)
    print("action_value:", action_value)

    result = {"object_value": object_value,"action_value":action_value}
    
    return jsonify(result)

if __name__ == '__main__':
    app.run(debug=True)