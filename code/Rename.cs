using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace esilhmt.code
{
    class Rename
    {
        static void Main(string[] args)
        {
            // 提示用户输入文件夹的路径
            // Prompt the user to enter the directory path
            Console.WriteLine("请输入文件夹的路径：");
            Console.WriteLine("Please enter the directory path:");
            string directoryPath = Console.ReadLine();

            // 遍历指定文件夹中的所有.md文件
            // Iterate through all .md files in the specified directory
            string[] mdFiles = Directory.GetFiles(
                directoryPath,
                "*.md",
                SearchOption.AllDirectories
            );

            int count = 0; // 用于跟踪处理的文件数量的计数器
            // Counter to keep track of the number of files processed

            foreach (string file in mdFiles)
            {
                string fileName = Path.GetFileName(file);
                string directory = Path.GetDirectoryName(file);

                // 使用正则表达式检查文件名是否符合指定的格式
                // Use regular expression to check if the file name matches the specified format
                if (Regex.IsMatch(fileName, @"^\d{2}\.\d{2}\.\d{4}\.md$"))
                {
                    // 替换点(.)为连字符(-)
                    // Replace dots(.) with hyphens(-)
                    string newFileName = fileName.Replace(".", "-");

                    // 将扩展名前的连字符(-)替换回点(.)
                    // Replace the hyphen(-) before the extension with a dot(.)
                    newFileName = newFileName.Insert(10, ".");

                    // 重命名文件
                    // Rename the file
                    File.Move(
                        Path.Combine(directory, fileName),
                        Path.Combine(directory, newFileName)
                    );

                    // 输出重命名的信息
                    // Output renaming information
                    Console.WriteLine($"修改 \"{fileName}\" 文件为 \"{newFileName}\"");
                    Console.WriteLine($"Renamed file \"{fileName}\" to \"{newFileName}\"");

                    count++; // 更新处理的文件数量
                    // Update the number of files processed
                }
            }

            // 输出处理完毕的信息
            // Output the completion information
            Console.WriteLine($"完成处理了 {count} 个文件。");
            Console.WriteLine($"Finished processing {count} files.");
        }
    }
}
