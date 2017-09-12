■ Mexif ver.1.1
Last update: 2012/11/12

指定フォルダに含まれる画像よりExif情報を取り出し
CSV形式で保存するツールです。
デジタルカメラの写真を想定しているため、対象とする形式は.jpgです。


■ 出力形式と例
FILENAME,TIMESTAMP,LAT_DEG,LAT_MIN,LAT_SEC,LON_DEG,LON_MIN,LON_SEC
IMG_5173.JPG,2012:07:04 10:11:38,36,28,0.6,123,6,45


■ 動作環境
Microsoft .NET Framework 2.0 以上。

■ 使い方
対象とする画像のフォルダを、コマンドライン引数で指定します。
引数の指定方法は以下。

> mexif.exe "対象とする画像を含むフォルダのフルパス" 

また引数の後ろに「 > "C:\mexif.csv"」等、追記することにより
別ファイルに出力を保存することができます。

保存場所はフルパスで指定しない場合、MS-DOSのカレントディレクトリに
なります。（C:\Users\hoge など）

一括処理の際に便利です。

■ コンパイル
プロジェクトに追加しても良いですが、下記コマンドでコンパイル可能です。
> c:\Windows\Microsoft.NET\Framework\v2.0.50727\csc.exe mexif.cs

mexif.exe が作成されます。
