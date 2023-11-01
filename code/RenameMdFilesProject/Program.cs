using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace esilhmt.code
{
    class Program
    {
        static void Main()
        {
            // 打印提示信息 / Print prompt message
            Console.WriteLine(
                "请输入要处理的文件夹名称： / Please enter the name of the folder to be processed:"
            );

            string folderPath = Console.ReadLine();

            // 循环直到用户输入有效的文件夹路径 / Loop until user inputs a valid folder path
            while (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
            {
                Console.WriteLine(
                    "请重新输入文件夹的绝对地址： / Please re-enter the absolute address of the folder:"
                );
                folderPath = Console.ReadLine();
            }

            // 重命名文件并获取重命名文件的数量 / Rename files and get the number of renamed files
            int count = RenameFilesInFolder(folderPath);

            // 打印完成信息 / Print completion message
            Console.WriteLine($"完成处理了{count}个文件。 / Processed {count} files.");

            // 等待用户按键以结束程序 / Wait for user input to end the program
            Console.ReadLine();
        }

        // 重命名文件夹内的文件方法 / Method to rename files inside the folder
        static int RenameFilesInFolder(string folderPath)
        {
            int count = 0;

            // 遍历文件夹中的所有.md文件 / Traverse all .md files in the folder
            foreach (
                var file in Directory.GetFiles(folderPath, "*.md", SearchOption.AllDirectories)
            )
            {
                string fileName = Path.GetFileName(file);

                // 使用正则表达式检查文件名是否符合给定的模式 / Check if the filename matches the given pattern using regular expression
                if (Regex.IsMatch(fileName, @"^\d{2}\.\d{2}\.\d{4}\.md$"))
                {
                    string newFileName = fileName.Replace(".", "-");

                    // 获取要替换的位置 / Get the position to be replaced
                    int positionToReplace = newFileName.Length - 3;

                    // 在指定位置替换符号 / Replace symbol at the specified position
                    newFileName = newFileName
                        .Remove(positionToReplace, 1)
                        .Insert(positionToReplace, ".");

                    // 如果新文件名与原文件名不同，则进行重命名 / Rename if the new filename is different from the original filename
                    if (fileName != newFileName)
                    {
                        string directory = Path.GetDirectoryName(file);

                        if (directory != null)
                        {
                            string newPath = Path.Combine(directory, newFileName);
                            File.Move(file, newPath);

                            // 打印重命名信息 / Print renaming information
                            Console.WriteLine(
                                $"修改 \"{fileName}\" 文件为 \"{newFileName}\" / Renamed \"{fileName}\" to \"{newFileName}\""
                            );

                            count++;
                        }
                    }
                }
            }

            // 返回重命名文件的数量 / Return the number of renamed files
            return count;
        }
    }
}
