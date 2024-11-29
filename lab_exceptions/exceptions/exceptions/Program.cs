namespace exceptions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double sum = 0;
            int count = 0;

            List<string> noFile = new List<string>();
            List<string> badData = new List<string>();
            List<string> overflow = new List<string>();
            List<int> validProducts = new List<int>();

            try
            {
                string folderPath = @"C:\Users\Lenovo\Desktop\Proba\createFiles";

                string[] filenames = new string[20];
                for (int i = 0; i < 20; i++)
                {
                    filenames[i] = $"{10 + i}.txt";

                    try
                    {
                        string filePath = Path.Combine(folderPath, $"{filenames[i]}.txt");
                        string[] lines = File.ReadAllLines(filePath);
                        int num1 = int.Parse(lines[0].Trim());
                        int num2 = int.Parse(lines[1].Trim());

                        try
                        {
                            checked
                            {
                                int multiply = num1 * num2;
                                validProducts.Add(multiply);
                                sum += multiply;
                                count++;
                            }
                        }
                        catch (OverflowException)
                        {
                            overflow.Add(filenames[i]);
                        }
                    }
                    catch (FileNotFoundException)
                    {
                        noFile.Add(filenames[i]);
                    }
                    catch (Exception)
                    {
                        badData.Add(filenames[i]);
                    }
                }

                File.WriteAllLines(Path.Combine(folderPath, "no_file.txt"), noFile);
                File.WriteAllLines(Path.Combine(folderPath, "bad_data.txt"), badData);
                File.WriteAllLines(Path.Combine(folderPath, "overflow.txt"), overflow);

                try
                {
                    double average = sum / count;
                    Console.WriteLine($"Середнє арифметичне: {average}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            catch
            {
                Console.WriteLine("Помилка створення або запису у файли результатів.");
                return;
            }
        }
    }
}