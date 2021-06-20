---
# AsyncDesignPattern
勉強用です

---
# 準備してほしいもの
* Docker Desktopのインストール
  * あってもなくてもどっちでもよいですが、コンテナで実行したい人は是非
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
クライアントとサーバーを同時に起動 -> クライアントからサーバーに対してタスク実行の依頼を投げ続けます。<br>
サーバーのタスクの実行において非同期のデザインパターンを使用して実装してみましょう。<br>
というスタンスです。

通信はソケットで行っています。<br>
(自分の勉強用としてソケットを使用しています…コード量が増えてしまいましてすみません)

## プロジェクト構成
* AsyncDesignPattern.Client
  * クライアントのプロジェクトです
  * Worker Serviceのプロジェクトです
* AsyncDesignPattern.Server
  * サーバーのプロジェクトです 
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
  * 合計13のパターンの実装予定です
  * クラスライブラリです

---
# 進め方

1. デザインパターンの概要理解
2. デザインパターンのクラス図にての理解
3. デザインパターンの実装
4. デバッグ
5. 意見出し。実務においてどのように使えるか、メリットデメリットetc...
