import sys

# 检查命令行参数的数量
if len(sys.argv) != 2:
    print("使用方法: python add_spaces.py [filename]")
    sys.exit(1)

# 从命令行参数获取文件名
filename = sys.argv[1]

# 使用utf-8编码方式读取文件内容
with open(filename, 'r', encoding='utf-8') as file:
    content = file.readlines()

# 使用utf-8编码方式为每行的末尾添加两个空格，并重新写入文件
with open(filename, 'w', encoding='utf-8') as file:
    for line in content:
        file.write(line.rstrip('\n') + '  \n')

# 输出处理的行数和完成信息
print(f"处理了 {len(content)} 行。")
print("Finished all process。")
