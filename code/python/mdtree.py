import os


def mdtree(directory, prefix="", startpath=None):
    """生成给定目录的树状图（Markdown格式）"""

    # 如果是初始调用，设置startpath
    if startpath is None:
        startpath = directory

    # 获取目录下的所有内容列表
    entries = os.listdir(directory)
    entries = sorted(entries, key=lambda x: x.lower())  # 对列表进行排序

    tree = []
    for entry in entries:
        path = os.path.join(directory, entry)
        relative_path = os.path.relpath(path, startpath)  # 获取相对路径

        # 根据是文件还是文件夹，使用不同的Markdown格式
        if os.path.isdir(path):
            tree.append(f"{prefix}- **{entry}/**")
            tree.extend(mdtree(path, prefix + "  ", startpath))
        else:
            tree.append(f"{prefix}- [{entry}]({relative_path})")

    return tree


if __name__ == "__main__":
    directory_to_scan = input("请输入要扫描的目录: ")
    md_tree = mdtree(directory_to_scan)

    with open("output.md", "w") as f:
        for line in md_tree:
            f.write(line + "\n")

    print("树状图已保存到 output.md 文件中。")
