---
# AsyncDesignPattern
勉強用です<br>
非同期実装のデザインパターンをC#にて学びます。<br>
ここの内容が今後非同期実装するときの引き出しとなればよいかなと考えています。<br>
ThreadではなくTaskを使います。<br>

以下の書籍を元ネタとした内容となっています。(みんな大好き 結城 浩さんです)<br>

[増補改訂版 Java言語で学ぶデザインパターン入門 マルチスレッド編](https://www.amazon.co.jp/%E5%A2%97%E8%A3%9C%E6%94%B9%E8%A8%82%E7%89%88-Java%E8%A8%80%E8%AA%9E%E3%81%A7%E5%AD%A6%E3%81%B6%E3%83%87%E3%82%B6%E3%82%A4%E3%83%B3%E3%83%91%E3%82%BF%E3%83%BC%E3%83%B3%E5%85%A5%E9%96%80-%E3%83%9E%E3%83%AB%E3%83%81%E3%82%B9%E3%83%AC%E3%83%83%E3%83%89%E7%B7%A8-%E7%B5%90%E5%9F%8E-%E6%B5%A9/dp/4797331623)

---
# 環境について
## 動作環境
* docker runtime(dockerで動かしたい人だけ)
* .NET 6(sdk入れるのがめんどくさい人はバージョン下げても大丈夫です。多分動きます。)
## 使用ツール
* visual studio (バージョンはなんでもよいですが、2022preview版だと.NET6のランタイムが初めから入っています。あとコンテナツール便利です。)
* docker desktop(composeが使えればよいです。)

---
# 準備してほしいもの
* gitアカウント
  * Forkして使っていただこうかと考えています。めんどくさければブランチここに作っちゃってもよいです。
* Docker Desktopのインストール
  * あってもなくてもどっちでもよいですが、コンテナで実行したい人はあると楽です。
  * https://www.docker.com/products/docker-desktop
* visual sutdio preview機能の有効化
  * .NET　6で動かすために必要です。(Use Preview of .NET Core)
  * https://docs.microsoft.com/ja-jp/visualstudio/ide/reference/environment-preview-features-options?view=vs-2019
* .NET 6 sdkのインストール
  * sdkが必要です
  * https://dotnet.microsoft.com/download/visual-studio-sdks

---
# プロジェクトについて
## はじめに
非同期タスクを実行する方法として以下の3つを考えています・

1. Entrypointからの実行<br>
Mainメソッドから1タスクを実行して動きを見てみます
2. Worker Serviceからの実行<br>
whileでたくさんタスクを実行してみます 
3. クラサバアプリでの実行<br>
クライアントとサーバーを同時に起動 -> クライアントからサーバーに対してタスク実行の依頼を投げ続けます。<br>
サーバーのタスクの実行において非同期のデザインパターンを使用して実装してみましょう。<br>
というスタンスです。

自分の勉強を兼ねてクラサバアプリではソケットを使用しています…コード量が増えてしまいましてすみません)

## プロジェクト構成
* AsyncDesignPattern.Executor
  * はじめに > 2. Worker Serviceからの実行　にて使用します
  * Worker Serviceのプロジェクトです
* AsyncDesignPattern.Client
  * はじめに > 3. クラサバアプリでの実行　にて使用します
  * Worker Serviceのプロジェクトです
* AsyncDesignPattern.Server
  * はじめに > 3. クラサバアプリでの実行　にて使用します
  * Worker Serviceのプロジェクトです
* AsyncDesignPattern.SenderReciever
  * 通信のクライアント部分とリスナー部分のプロジェクトです
  * クラスライブラリです
* AsyncDesignPattern.TaskFamily
  * Task処理に関するプロジェクトです
  * クラスライブラリです
* AsyncDesignPattern.Repository
  * リポジトリです。
  * .NET 5 からrecordという修飾子が追加されました。classやstructみたいなもので、Equalsの挙動が変わっています。(便利そうなので勉強用として入れ込んでいます)
  * クラスライブラリです
* AsyncDesignPattern.Common
  * Commonです
  * クラスライブラリです

* DesignPatternsフォルダ配下
  * ここには書くデザインパターンの実装プロジェクトが配置されています。
  * エントリポイントを用意してありますので最初はここで動作確認してください。※はじめに > 1. Entrypointからの実行 にて使います
  * 合計13のパターンの実装予定です
  * クラスライブラリです

---
# 進め方

1. デザインパターンの概要理解
2. デザインパターンのクラス図にての理解
3. デザインパターンの実装
 1. エントリポイントから実行します(並列ではなく単一で動きを見ます)
 2. Worker Serviceから並列実行をします
 3. (やりたい人だけ)クラサバの方から並列実行をします
4. 意見出し。先輩に色んなこと聞いてみよう。実務においてどのように使えるか、メリットデメリットetc...
