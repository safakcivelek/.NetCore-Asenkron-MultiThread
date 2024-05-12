using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskFormApp
{
    public partial class Form1 : Form
    {
        public int counter { get; set; } = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private async void BtnReadFile_Click(object sender, EventArgs e)
        {
            //Senkron :
            //string data = ReadFile(); 
            //richTextBox1.Text = data;

            //Asenkron :
            //Burada "await" kullandık çünkü artık bir alt satırda bu datayla ilgili işlem yapıyoruz.
            //Bu işlem 15sn'de sürse await ile burada bekleyecek, fakat ana Thread'i durdurmayacak o işine devam edecek.
            string data = await ReadFileAsync();
            richTextBox1.Text = data;
        }

        private void BtnCounter_Click(object sender, EventArgs e)
        {
            textBoxCounter.Text = counter++.ToString();
        }

        //Senkron :
        private string ReadFile()
        {
            string data = string.Empty;
            using (StreamReader s = new StreamReader("dosya.txt"))
            {
                Thread.Sleep(5000); //Bu metod ana thread'i bloklar!
                data = s.ReadToEnd();
            }

            return data;
        }

        //Asenkron :
        //Eğer asenkron metod geriye birşey dönmeyecekse sadece 'Task' keywordünü kullanabilirim.
        //Task, senkron metoddaki void'e karşılık gelir.
        //Eğer Asenkron metottan bir data döneceksem Task<string> şeklinde belirtebilirim.
        //  ( void => Task  // string Task<string> )

        //async => derleyici async keywordünü gördüğü zaman bu metodun içerisinde bir asenkron metod çağrımı olduğunu anlayack.
        private async Task<string> ReadFileAsync()
        {
            string data = string.Empty;
            using (StreamReader s = new StreamReader("dosya.txt"))
            {
                Task<string> myTask = s.ReadToEndAsync();
                // Bu aralıkta ReadToEndAsync() metodundan dönecek datayla ilgisi olmayan işler yapabilirm.
                // 10sn vb. süren işlemler olabilir...
                //-------------------------------

                //Burada 5 sn geçikecek, yanlız burada benim ana thread'imi bloklamıyor.
                //5sn lik asenkron işlem smilasyonu olarak düşünülebilir.
                //await ile'de işaretledimki 5sn süresince burada beklenecek, bloklanacak.
                await Task.Delay(5000);

                data = await myTask;

                return data;
            }
        }
    }
}
