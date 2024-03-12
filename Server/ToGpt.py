from openai import OpenAI

client = OpenAI(
    # defaults to os.environ.get("OPENAI_API_KEY")
    api_key="sk-UBy8u92t9L4vVG39w2P1vUZw4Colo8ZLUejlwtYUO53sFDG2",
    base_url="https://api.chatanywhere.tech/v1"
)



# 非流式响应
def gpt_35_api(messages: list):
    """为提供的对话消息创建新的回答

    Args:
        messages (list): 完整的对话消息
    """

    completion = client.chat.completions.create(model="gpt-3.5-turbo", messages=messages)
    result=completion.choices[0].message.content
    print(result)

    return result

def gpt_35_api_stream(messages: list):
    """为提供的对话消息创建新的回答 (流式传输)

    Args:
        messages (list): 完整的对话消息
    """
    stream = client.chat.completions.create(
        model='gpt-3.5-turbo',
        messages=messages,
        stream=True,
    )
    for chunk in stream:
        if chunk.choices[0].delta.content is not None:
            print(chunk.choices[0].delta.content, end="")


def togpt(msg,message_object,message_motion):
    # str1="对文本进行语义分析，识别出对象和动作，对象包括[MountainPuma]，动作包括[licking,standing,stretching,laying,roar,walk]，对象或动作不能为空，未识别到时，对象应该根据生物分类学选择最相似动物，动作应该使用最相似动作。返回结果名称必须与给定名称相同。结果只输出[object,action]\n"
    str1="对文本进行语义分析，识别出对象和动作，"
    str1+="对象包括["+message_object+"]，"
    str1+="动作包括["+message_motion+"]，"    
    str1+="对象或动作不能为空，未识别到时，对象应该根据生物分类学选择最相似动物，动作应该使用最相似动作。要求结果必须与给出的对象或动作相同。结果只输出json格式的object,action\n"
    str1+="文本："+msg


    messages = [{'role': 'user','content': str1},]
    # 非流式调用
    return gpt_35_api(messages)

# if __name__ == '__main__':
#     str='''
# 假设你现在帮我对文本进行语义分析，转化为具体指令，如果文本中不包含某个指令，则该指令的值为null\n
# 指令包括：对象（字符串）、移动距离（float）、移动方向（角度）、移动目的地（字符串）、对象动作（字符串）\n
# 文本：小马要跑到左边的草地上
# '''

#     str1='''
#     对文本进行语义分析，识别出对象和动作，对象包括[MountainPuma,wolf]，动作包括[licking,standing,stretching,laying,roar,walk]，对象或动作不能为空，未识别到时应该使用相似项
#     文本：The lion combs his fur with his tongue\n\n
#     输出格式：
#     object:
#     action:
#     '''

#     messages = [{'role': 'user','content': str1},]
#     # 非流式调用
#     # gpt_35_api(messages)
#     # 流式调用
#     gpt_35_api_stream(messages)
    # togpt("The lion combs his fur with his tongue")
