from ToGpt import togpt
import json
from flask import Flask, request, jsonify, send_file
import os
import requests

app = Flask(__name__)

UPLOAD_FOLDER = 'unity_uploads'
app.config['UPLOAD_FOLDER'] = UPLOAD_FOLDER

TO3D_NUM=0

@app.route('/unity_msg', methods=['POST'])
def get_unity_msg():
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
    action_speed = databack["speed"]

    print("object_value:", object_value)
    print("action_value:", action_value)
    print("action_speed:", action_speed)

    result = {"object_value": object_value,"action_value":action_value,"action_speed":action_speed}
    
    return jsonify(result)

@app.route('/unity_img', methods=['POST'])
def get_unity_img():
    for key in request.files:
        print(f"Field name: {key}")

    if 'file' not in request.files:
        return 'No file part'

    file = request.files['file']
    if file.filename == '':
        return 'No selected file'

    if file:
        # file.save(os.path.join(app.config['UPLOAD_FOLDER'], file.filename))
        # print('File path: ',os.path.join(app.config['UPLOAD_FOLDER'], file.filename))

        url = 'http://127.0.0.1:5001/receive_image'  # 第二个Flask服务器的地址
        files = {'file': file.read()}  # 读取文件内容并放入表单数据中
        response = requests.post(url, data={'message': file.filename}, files=files)

        print("server2 response: ",response.text)

        return 'File uploaded successfully'
    
@app.route('/server2_img', methods=['POST'])
def server2_img():
    target_folder="unity_uploads"
    obj_file = request.files['file']
    obj_file.save(os.path.join(target_folder,'received_file.obj'))

    image_file = request.files['image']
    image_file.save(os.path.join(target_folder,'received_image.png'))

    return 'obj,img saved successfully.'

@app.route('/unity_obj', methods=['GET'])
def post_unity_obj():
    print("post obj")
    target_folder="unity_uploads"
    return send_file(os.path.join(target_folder,'received_file.obj'), as_attachment=True)

@app.route('/unity_mtl', methods=['GET'])
def post_unity_mtl():
    print("post mtl")
    target_folder="unity_uploads"
    return send_file(os.path.join(target_folder,'received_image.png'), as_attachment=True)


if __name__ == '__main__':
    app.run(port=5000,debug=False)