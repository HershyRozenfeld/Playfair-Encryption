namespace Playfair_Encryption
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char[,] mat = new char[5, 5];
            Console.Write("Pleas enter a key: ");
            string key = Console.ReadLine();
            char[] KeyChars = key.ToCharArray();//המרת הסטרינג למחרוזת תווים
            filling(mat, KeyChars);
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    Console.Write(" " + mat[i, j]);
                }
                Console.WriteLine();
            }
            Console.Write("Pleas enter your message: ");
            string message = Console.ReadLine();
            char[] MessChars = message.ToCharArray();
            string EncryStr = ArrangementCharacters(mat, MessChars);
            Console.Write("The text to be encrypted: ");
            for(int i = 0;i < EncryStr.Length; i++)
            {
                Console.Write(EncryStr[i]);
                if (i % 2 != 0)
                    Console.Write(" ");
            }
            char[] Encryp = Encryption(mat, EncryStr);
            Console.WriteLine();
            Console.Write("The encrypted text: ");
            for (int i = 0; i < Encryp.Length; i++)
            {
                Console.Write(Encryp[i]);
                if (i % 2 != 0)
                    Console.Write(" ");
            }
            Console.WriteLine();
        }
        static void filling(char[,] mat, char[] KeyChars)//פונקציה למילוי המטריצה בתווי המפתח
        {
            int l = 0;

            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    if (mat[i, j] == '\0')
                    {
                        if (l < KeyChars.Length)
                        {
                            for(int n=0; n<KeyChars.Length; n++)
                            {
                                if (KeyChars[l] != ' ')
                                {
                                    if (!IsCharPresentInMat(mat, KeyChars[l]))
                                    {
                                        mat[i, j] = KeyChars[l];
                                        n = KeyChars.Length;
                                    }
                                }
                                l++;//מונע העתקת תוי רווח

                                if (l == KeyChars.Length)
                                {//מונע יציאה מגבולות המערך

                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (int h = 0; h < 26; h++)
                            {
                                char abc = (char)(h + 97);//מביא את כל תווי האותיות 
                                if (abc == 'q')
                                    abc = 'r';
                                if (!IsCharPresentInMat(mat, abc))
                                {
                                    mat[i, j] = abc;
                                    break;
                                }
                            }
                        }

                    }
                }
            }
        }
        //פונקציית בדיקה האם תיו מופיע במערך
        static bool IsCharPresentInMat(char[,] mat, char targetChar)
        {
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    if (mat[i, j] == targetChar)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        static string ArrangementCharacters(char[,] mat, char[] MessChars)
        {//יצירת מחרוזת מההודעה, המוכנה להצפנה
            int n = 0;
            string EncryStr = "";
            for (int i = 0; i < MessChars.Length; i++)
            {
                if (MessChars[i] != ' ' && MessChars[i] != '\0')
                {       //מניעת שגיאת יציאה ממערך 
                    if (i < MessChars.Length - 1 && MessChars[i] != MessChars[i + 1])
                    {
                        EncryStr = EncryStr.Insert(n, MessChars[i] + "");
                        n++;
                        i++;
                        if (MessChars[i] == ' ' && i < MessChars.Length - 1)//החרגת תווי הרווח 
                        {
                            i++;
                        }  
                        EncryStr = EncryStr.Insert(n, MessChars[i] + "");
                        n++;
                    }
                    else
                    {
                        EncryStr = EncryStr.Insert(n, MessChars[i] + "");
                        n++;
                        EncryStr = EncryStr.Insert(n, "x");
                        n++;
                    }
                } 
            }
            return EncryStr;
        }
        static char[] Encryption(char[,] mat, string EncryStr)
        {//פונקציה המצפינה את מחרוזת התווים המקורית למחרוזת חדשה מוצפנת
            char[] EncryArr = EncryStr.ToCharArray();
            for (int n = 0; n < EncryArr.Length; n++)
            {
                int i1 = -1, j1 = -1, i2 = -1, j2 = -1;
                for (int i = 0; i < mat.GetLength(0); i++)
                {
                    for (int j = 0; j < mat.GetLength(1); j++)
                    {
                        if (j < mat.GetLength(1) - 2 && EncryArr[n] == mat[i, j] && EncryArr[n + 1] == mat[i, j + 1])
                        {
                            EncryArr[n] = mat[i, j + 1];
                            EncryArr[n + 1] = mat[i, j + 2];
                            j = mat.GetLength(1);
                            i = mat.GetLength(0);
                            break;
                        }
                        else if (j > 1 && EncryArr[n] == mat[i, j] && EncryArr[n + 1] == mat[i, j - 1])
                        {
                            EncryArr[n] = mat[i, j - 1];
                            EncryArr[n + 1] = mat[i, j - 2];
                            j = mat.GetLength(1);
                            i = mat.GetLength(0);
                            break;
                        }
                        else if (i < mat.GetLength(0) - 2 && EncryArr[n] == mat[i, j] && EncryArr[n + 1] == mat[i + 1, j])
                        {
                            EncryArr[n] = mat[i + 1, j];
                            EncryArr[n + 1] = mat[i + 2, j];
                            j = mat.GetLength(1);
                            i = mat.GetLength(0);
                            break;
                        }
                        else if (i > 1 && EncryArr[n] == mat[i, j] && EncryArr[n + 1] == mat[i - 1, j])
                        {
                            EncryArr[n] = mat[i - 1, j];
                            EncryArr[n + 1] = mat[i - 2, j];
                            j = mat.GetLength(1);
                            i = mat.GetLength(0);
                            break;
                        }
                        else
                        {
                            if (EncryArr[n] == mat[i, j])
                            {
                                i1 = i;
                                j1 = j;
                            }
                            if (EncryArr[n + 1] == mat[i, j])
                            {
                                i2 = i;
                                j2 = j;
                            }
                            if (i1 > -1 && i2 > -1)
                            {
                                EncryArr[n] = mat[i1, j2];
                                EncryArr[n + 1] = mat[i2, j1];
                                j = mat.GetLength(1);
                                i = mat.GetLength(0);
                            }
                        }
                    }
                }
                n++;
            }
            return EncryArr;
        }

    }
}
