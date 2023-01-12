
                             READMEJ.TXT  File
           README file for FANUC Drivers and Libraries (FOCAS1/2)
       (C) Copyright FANUC CORPORATION, 1996-2015. All Rights Reserved.


  このドキュメントには FANUC Drivers and Libraries (FOCAS1/2) に関する
  以下の情報が記述されています。
  ご使用の前に必ずお読みいただくようお願いいたします。

  (*) FOCAS1/2 : FANUC Open CNC API Specifications version 1 or 2

--------------------------------------------------------------------------
[1] CD-ROMの構成について
[2] CD-ROMの内容について
[3] ファイルの概要について
[4] 動作環境について
[5] インストール方法について
[6] ソフトウェアの再配布について
[7] 更新履歴
--------------------------------------------------------------------------

  Microsoft, MS, MS-DOS, Visual Basic, Visual C++, Windows, Win32, Win32s,
  Windows NT, Widnows Vista は米国Microsoft社の登録商標です。
  その他、記載されている会社名、製品名は各社の登録商標、または商標です。

  (*) 本CD-ROM内のHSSB用のソフトウェアは高速シリアルバス(HSSB)で接続された
      パソコン、FS160i/180i/210i、FANUC PANEL i および、パソコン機能付CNC表示
      ユニット用です。
      MMC-IV、高速シリアルバスType 1で接続されたパソコン、インテリジェン
      トターミナル用に開発されたものではありません。


==========================================================================
[1] CD-ROMの構成について
==========================================================================

    本CD-ROM中のライブラリは、以下の開発言語で使用することができます。
      - 32ビット版
          Visual C++ 2.x/4.x/5.x/6.x, Visual studio 2002/2003/2005/2008/2010
          Visual Basic 4.x/5.x/6.x
      - 64ビット版（Winows 7 Professional 64ビット版でのみ動作）
          Visual studio 2010
      - Linux版（Ubuntu 12.04 LTS, Fedora 17, CentOS 6.3でのみ動作）
          GNU Compiler Collection（4.6.3版以降）


==========================================================================
[2] CD-ROMの内容について
==========================================================================

    このCD-ROMは以下のようなフォルダ構造になっています。必要なファイルをコピ
    ーして下さい。

    詳しくは
        [5] インストール方法について
    をお読みください。

    README.TXT  : 英語版ドキュメント
    READMEJ.TXT : このファイル


    * HSSBドライバ関係のファイル(32/64ビット版)

     <HSSB>\README2.TXT   : HSSBドライバ英語ドキュメント
     <HSSB>\README2J.TXT  : HSSBドライバ日本語ドキュメント
     <HSSB>\NCBOOT32.DOC  : NCBOOT32.EXE 英語ドキュメント(32/64ビット共通)
     <HSSB>\NCBOOT32J.DOC : NCBOOT32.EXE 日本語ドキュメント(32/64ビット共通)
     <HSSB>\UPDATE.EXE    : HSSBドライバアップデート用ユーティリティ
     <HSSB\WIN95>\*.*     : Windows 95/98用HSSBドライバ
     <HSSB\NT40>\*.*      : Windows NT 4.0用HSSBドライバ
     <HSSB\WIN2K>\*.*     : Windows 2000用HSSBドライバ
     <HSSB\WINXP>\*.*     : Windows XP用HSSBドライバ
     <HSSB\COMMON>\*.*    : Windows 95/98/NT/2000/XP共通ファイル
     <HSSB\VISTA>\*.*     : Windows Vista/7 32ビット版用HSSBドライバ
     <HSSB\WIN7_64>\*.*   : Windows 7 64ビット版用HSSBドライバ

        * ドキュメントファイルは、Microsoft Word 2000以降で開いて下さい。
          ワードパッドでは、開けない場合があります。


    * CNC/PMCデータウィンドウライブラリ関連のファイル(32ビット版)

     <FWLIB>\FWLIB32.DLL  : CNC/PMCデータウィンドウ統轄ライブラリ
     <FWLIB>\FWLIBE1.DLL  : 機種別ライブラリ(TCP/IP用)
     <FWLIB>\FWLIB30i.DLL : 機種別ライブラリ(30i/31i/32i/35i/0i-F,PMi-A用)
     <FWLIB>\FWLIB150.DLL : 機種別ライブラリ(150-B用)
     <FWLIB>\FWLIB15i.DLL : 機種別ライブラリ(150i用)
     <FWLIB>\FWLIB160.DLL : 機種別ライブラリ(160/180/210用)
     <FWLIB>\FWLIB16W.DLL : 機種別ライブラリ(160i/180i-W用)
     <FWLIB>\FWLIB0i.DLL  : 機種別ライブラリ(0i-A用)
     <FWLIB>\FWLIB0iB.DLL : 機種別ライブラリ(0i-B用)
     <FWLIB>\FWLIB0iD.DLL : 機種別ライブラリ(0i-D用)
     <FWLIB>\FWLIBPM.DLL  : 機種別ライブラリ(Power Mate-D/H用)
     <FWLIB>\FWLIBPMi.DLL : 機種別ライブラリ(Power Mate i-D/H用)
     <FWLIB>\FWLIBNCG.DLL : 機種別ライブラリ(FS31/32i/35i NCGuidePro用)
     <FWLIB>\FWLIB0DN.DLL : 機種別ライブラリ(FS0i-D NCGuidePro用)
     <FWLIB>\FWLIB32.LIB  : インポートライブラリ
     <FWLIB>\FWPMCALM.INI : PMCのアラームメッセージファイル
     <FWLIB\LINUX\ARM>\libfwlib32.so.1.0.1 
     <FWLIB\LINUX\X86>\libfwlib32.so.1.0.1 
                          : 機種別ライブラリ(Linux用)


    * CNC/PMCデータウィンドウ関数，構造体用
                                  C/C++、Visual Basicヘッダファイル(32ビット版)

     <FWLIB>\FWSYMBOL.H      : シンボル定義ファイル(共通)
     <FWLIB\e1>\FWLIB32.H    : TCP/IP用
               (FS15i/16i/18i/21i/30i/31i/32i/35i/0i-B/0i-C/0i-D/0i-F,
                Power Mate i, PMi-A, FS31/32i/35i/0i-D NCGuidePro 共通)
               CNC/PMCデータウィンドウ関数，構造体用C/C++/C++ .NETヘッダ
               ファイル
     <FWLIB\e1>\FWLIB32.BAS  : TCP/IP用
               (FS15i/16i/18i/21i/30i/31i/32i/35i/0i-B/0i-C/0i-D/0i-F, 
                Power Mate i, PMi-A, FS31/32i/35i/0i-D NCGuidePro 共通)
               CNC/PMCデータウィンドウ関数，構造体用Visual Basicヘッダ
               ファイル
     <FWLIB\30i>\FWLIB32.H   : FS30i/31i/32i/35i/0i-F, PMi-A, FS31i/32i/35i
               NCGuidePro用 CNC/PMCデータウィンドウ関数，構造体用C/C++ヘッダ
               ファイル
     <FWLIB\30i>\FWLIB32.BAS : FS30i/31i/32i/35i/0i-F, PMi-A, FS31i/32i/35i
               NCGuidePro用 CNC/PMCデータウィンドウ関数，構造体用Visual Basic
               ヘッダファイル
     <FWLIB\150>\FWLIB32.H   : FS150-B用
               CNC/PMCデータウィンドウ関数，構造体用C/C++ヘッダファイル
     <FWLIB\150>\FWLIB32.BAS : FS150-B用
               CNC/PMCデータウィンドウ関数，構造体用Visual Basicヘッダ
               ファイル
     <FWLIB\15i>\FWLIB32.H   : FS150i用
               CNC/PMCデータウィンドウ関数，構造体用C/C++ヘッダファイル
     <FWLIB\15i>\FWLIB32.BAS : FS150i用
               CNC/PMCデータウィンドウ関数，構造体用Visual Basicヘッダ
               ファイル
     <FWLIB\160>\FWLIB32.H   : FS160/180-B/C, FS210-B, FS160i/180i/210i用
               CNC/PMCデータウィンドウ関数，構造体用C/C++ヘッダファイル
     <FWLIB\160>\FWLIB32.BAS : FS160/180-B/C, FS210-B, FS160i/180i/210i用
               CNC/PMCデータウィンドウ関数，構造体用Visual Basicヘッダ
               ファイル
     <FWLIB\16iW>\FWLIB32.H   : FS160i/180i-W用
               CNC/PMCデータウィンドウ関数，構造体用C/C++ヘッダファイル
     <FWLIB\16iW>\FWLIB32.BAS : FS160i/180i-W用
               CNC/PMCデータウィンドウ関数，構造体用Visual Basicヘッダ
               ファイル
     <FWLIB\0i>\FWLIB32.H   : FS0i-A用
               CNC/PMCデータウィンドウ関数，構造体用C/C++ヘッダファイル
     <FWLIB\0i>\FWLIB32.BAS : FS0i-A用
               CNC/PMCデータウィンドウ関数，構造体用Visual Basicヘッダ
               ファイル
     <FWLIB\0iB>\FWLIB32.H   : FS0i-B用
               CNC/PMCデータウィンドウ関数，構造体用C/C++ヘッダファイル
     <FWLIB\0iB>\FWLIB32.BAS : FS0i-B用
               CNC/PMCデータウィンドウ関数，構造体用Visual Basicヘッダ
               ファイル
     <FWLIB\0iD>\FWLIB32.H   : FS0i-D、FS0i-D NCGuidePro用
               CNC/PMCデータウィンドウ関数，構造体用C/C++ヘッダファイル
     <FWLIB\0iD>\FWLIB32.BAS : FS0i-D、FS0i-D NCGuidePro用
               CNC/PMCデータウィンドウ関数，構造体用Visual Basicヘッダ
               ファイル
     <FWLIB\PM>\FWLIB32.H   : Power Mate-H, Power Mate-D用
               CNC/PMCデータウィンドウ関数，構造体用C/C++ヘッダファイル
     <FWLIB\PM>\FWLIB32.BAS : Power Mate-H, Power Mate-D用
               CNC/PMCデータウィンドウ関数，構造体用Visual Basicヘッダ
               ファイル
     <FWLIB\PMI>\FWLIB32.H   : Power Mate i-D, Power Mate i-H用
               CNC/PMCデータウィンドウ関数，構造体用C/C++ヘッダファイル
     <FWLIB\PMI>\FWLIB32.BAS : Power Mate i-D, Power Mate i-H用
               CNC/PMCデータウィンドウ関数，構造体用Visual Basicヘッダ
               ファイル

     <FWLIB\Dot NET sample>\FWLIB32.CS : Visual C# .NET用サンプル
               Visual C# .NET用ヘッダファイルのサンプルです。
               必要に応じて内容を修正してご使用下さい。
     <FWLIB\Dot NET sample>\FWLIB32.VB : Visual Basic .NET用サンプル
               Visual Basic .NET用ヘッダファイルのサンプルです。
               必要に応じて内容を修正してご使用下さい。
     <FWLIB\Dot NET sample>\DotNet.TXT  : .NET用ヘッダファイル英語ドキュメント
     <FWLIB\Dot NET sample>\DotNetJ.TXT : .NET用ヘッダファイル日本語ドキュメント

     <FWLIB\LINUX>\FWLIB32.H   : Linux用
               (FS30i/31i/32i/35i/0i-D, PMi-A 共通)
               CNC/PMCデータウィンドウ関数，構造体用C/C++ヘッダファイル

    * CNC/PMCデータウィンドウライブラリ関連のファイル(64ビット版)

     <FWLIB64>\FWLIB64.DLL    : CNC/PMCデータウィンドウ統轄ライブラリ
     <FWLIB64>\FWLIBE64.DLL   : 機種別ライブラリ(TCP/IP用)
     <FWLIB64>\FWLIB30i64.DLL : 機種別ライブラリ(FS30i/31i/32i/35i,0i-F,PMi-A用)
     <FWLIB64>\FWLIB0iD64.DLL : 機種別ライブラリ(0i-D用)
     <FWLIB64>\FWLIBNCG64.DLL : 機種別ライブラリ(FS31i/32i/35i NCGuidePro用)
     <FWLIB64>\FWLIB0DN64.DLL : 機種別ライブラリ(FS0i-D NCGuidePro用)
     <FWLIB64>\FWLIB64.LIB    : インポートライブラリ

        * 64ビット版は、FOCAS2(FS30i/31i/32i/35i,0i-F,PMi-A,FS0i-D用)のみの
          サポートです。


    * CNC/PMCデータウィンドウ関数，構造体用C/C++ヘッダファイル(64ビット版)

     <FWLIB64>\FWSYMBOL.H    : シンボル定義ファイル(共通)
     <FWLIB64\e1>\FWLIB64.H  : TCP/IP用
               (FS30i/31i/32i/35i/0i-D/0i-F, PMi-A, FS31i/32i/35i/0i-D NCGuidePro
               共通)CNC/PMCデータウィンドウ関数，構造体用C/C++ヘッダファイル
     <FWLIB64\30i>\FWLIB64.H : FS30i/31i/32i/35i/0i-F、PMi-A, FS31i/32i/35i
               NCGuidePro 用CNC/PMCデータウィンドウ関数，構造体用C/C++ヘッダ
               ファイル
     <FWLIB64\0iD>\FWLIB64.H : FS0i-D、、FS0i-D NCGuidePro 用
               CNC/PMCデータウィンドウ関数，構造体用C/C++ヘッダファイル


    * MDIキードライバ関係のファイル(32ビット版のみ)

      本CD-Rには、MDIキードライバは含まれていません。
      MDIキードライバは、Drivers Disk の A02B-0207-K793、A08B-0084-K790 に収め
      られています。
      MDIキードライバをWindows Vista Businessまたは、Windows 7 Professionalで使
      用する場合、A08B-0084-K790の01.8版以降が必要です。


    * 関数仕様書関係のファイル

     <DOCUMENT\SPECE>\*.*       : 英語版データウィンドウライブラリ仕様書
     <DOCUMENT\SPECJ>\*.*       : 日本語版データウィンドウライブラリ仕様書
     <DOCUMENT\PM>\FWLIBPM.TXT  : 英語版Power Mateライブラリ仕様書
     <DOCUMENT\PM>\FWLIBPMJ.TXT : 日本語版Power Mateライブラリ仕様書
     <DOCUMENT\NCG>\NCガイド･プロ FOCAS2機能.pdf
                                : 日本語版 NCガイド・プロ FOCAS2機能取扱説明書
     <DOCUMENT\NCG>\NCGuidePro FOCAS2 Function.pdf
                                : 英語版   NCガイド・プロ FOCAS2機能取扱説明書
     <DOCUMENT\LINUX>\FOCAS2_Linuxj.pdf
                                : 日本語版 Linux用FOCAS2/Ethernet取扱説明書
     <DOCUMENT\LINUX>\FOCAS2_Linux.pdf
                                : 英語版   Linux用FOCAS2/Ethernet取扱説明書


    * ユーティリティ関係のファイル

     <UTILITY>\CONTMEM.EXE  : CNCのメモリを表示するユーティリティ
     <UTILITY>\READMEC.TXT  : メモリ表示ユーティリティ英語ドキュメント
     <UTILITY>\READMECJ.TXT : メモリ表示ユーティリティ日本語ドキュメント
     <UTILITY>\OPTSET.EXE   : CNCのオプションを設定するユーティリティ
     <UTILITY>\README4.TXT  : オプション設定ユーティリティ英語ドキュメント
     <UTILITY>\README4J.TXT : オプション設定ユーティリティ日本語ドキュメント
     <UTILITY>\NUMLOCK.EXE  : キーボードのNumLockをON/OFFするツール
     <UTILITY>\NUMLOCK.TXT  : NUMLOCK.EXEの英語ドキュメント
     <UTILITY>\NUMLOCKJ.TXT : NUMLOCK.EXEの日本語ドキュメント


    * ログ出力関係のファイル

     <LOG>\FWLOG.EXE    : データウィンドウライブラリ通信ログ出力表示
                          ユーティリティ
     <LOG>\LOGLEVEL.EXE : データウィンドウライブラリ通信ログ出力機能制御
                          ユーティリティ

    * 大容量メモリからのプログラム運転関係のファイル

     <NCPROG>\FBINBUILD\FBINBUILD.EXE   : プログラム記憶ファイル編集ツール
     <NCPROG>\NCPROGCTRL\NCPROGCTRL.EXE : プログラム運転制御用ソフトウェア
     <NCPROG>\NCPROGDLL\WIN32\          : プログラム記憶ファイル編集用ライブラリ
                                                                   (32ビット版)
     <NCPROG>\NCPROGDLL\X64\            : プログラム記憶ファイル編集用ライブラリ
                                                                   (64ビット版)

    * シリアル/FCA通信関係のファイル

     <SERIAL>\FCA32.DLL   : シリアル/FCA通信 DLL型ライブラリ
     <SERIAL>\FCA32.LIB   : シリアル/FCA通信 インポートライブラリ
     <SERIAL>\FCA32.H     : シリアル/FCA通信関数，構造体用C/C++ヘッダファイル
     <SERIAL>\FCA32.BAS   : シリアル/FCA通信関数，構造体用Visual Basicヘッダ
                            ファイル
     <SERIAL>\SERIAL.TXT  : 英語版シリアル/FCA通信ライブラリ仕様書
     <SERIAL>\SERIALJ.TXT : 日本語版シリアル/FCA通信ライブラリ仕様書


   * CNC主導メッセージ通知機能サーバ関係のファイル

     <UNMSG>\UMSGSERV.EXE : CNC主導メッセージ通知機能サーバ


==========================================================================
[3] ファイルの概要について
==========================================================================

    <HSSB>フォルダ中のファイルについては、README2J.TXTを参照してください。

    * 32ビット版 FOCAS1/2ライブラリ

      < FWLIB32.DLL >
        高速シリアルバス(HSSB)、及びイーサネットで接続されたパソコンとCNC/
        PMC間で各種データや情報を交換するためのライブラリです。

      < FWLIBE1.DLL >
        TCP/IP用のライブラリです。FWLIB32.DLLから呼び出されます。
        NCガイド・プロのFOCAS2/Ethernet用にも使用します。

      < FWLIB30i.DLL >
        FS30i/31i/32i/35i/0i-F, PMi-A用のライブラリです。FWLIB32.DLLから呼び出さ
        れます。

      < FWLIB150.DLL >
        FS150-B用のライブラリです。FWLIB32.DLLから呼び出されます。

      < FWLIB15i.DLL >
        FS150i用のライブラリです。FWLIB32.DLLから呼び出されます。

      < FWLIB160.DLL >
        FS160/180-B/C, FS210-B, FS160i/180i/210i用のライブラリです。
        FWLIB32.DLLから呼び出されます。

      < FWLIB16W.DLL >
        FS160i/180i-W用のライブラリです。FWLIB32.DLLから呼び出されます。

      < FWLIB0i.DLL >
        FS0i-A用のライブラリです。FWLIB32.DLLから呼び出されます。

      < FWLIB0iB.DLL >
        FS0i-B用のライブラリです。FWLIB32.DLLから呼び出されます。

      < FWLIB0iD.DLL >
        FS0i-D用のライブラリです。FWLIB32.DLLから呼び出されます。

      < FWLIBPM.DLL >
        Power Mate-D, Power Mate-H用のライブラリです。FWLIB32.DLLから呼び出さ
        れます。

      < FWLIBPMi.DLL >
        Power Mate i-D, Power Mate i-H用のライブラリです。FWLIB32.DLLから呼び
        出されます。

      < FWLIBNCG.DLL >
        FS31i/32i/35i NCガイド・プロ用FOCAS2/HSSBのライブラリです。FWLIB32.DLL
        から呼び出されます。

      < FWLIB0DN.DLL >
        FS0i-D NCガイド・プロ用FOCAS2/HSSBのライブラリです。FWLIB32.DLLから呼
        び出されます。

      < FWLIB32.LIB >
        C/C++言語において、CNC/PMCウィンドウライブラリFWLIB32.DLLとダイナミッ
        クリンクをするためのインポートライブラリです。

      < FWPMCALM.INI >
        pmc_rdalmmsg()関数で使用されるPMCのアラームメッセージファイルです。

      < FWLIB32.H >
        CNC/PMCデータウィンドウ関数、ならびに構造体用のC/C++ヘッダファイルで
        す。
        ファイルは、機種別に分かれています。

      < FWSYMBOL.H >
        CNC/PMCデータウィンドウ関数で使用するシンボルが定義されたファイルで
        す。ファイルは共通です。

      < FWLIB32.BAS >
        CNC/PMCデータウィンドウ関数、ならびに構造体用のVisual Basicヘッダ
        ファイルです。
        ファイルは、機種別に分かれています。

      < FWLIB32.CS >
        CNC/PMCデータウィンドウ関数、ならびに構造体用のVisual C# .NETヘッダ
        ファイルです。

      < FWLIB32.VB >
        CNC/PMCデータウィンドウ関数、ならびに構造体用のVisual Basic .NET ヘッ
        ダファイルです。

      < libfwlib32.so.1.0.1 >
        イーサネットで接続されたパソコンとCNC/PMC間で各種データや情報を交換す
        るためのライブラリです。
        本ライブラリは、Linux用です。

    * 64ビット版 FOCAS2ライブラリ

      < FWLIB64.DLL >
        高速シリアルバス(HSSB)、及びイーサネットで接続されたパソコンとCNC/PMC
        間で各種データや情報を交換するためのライブラリです。

      < FWLIBE64.DLL >
        TCP/IP用のライブラリです。FWLIB64.DLLから呼び出されます。

      < FWLIB30i64.DLL >
        FS30i/31i/32i/35i/0i-F, PMi-A用のライブラリです。FWLIB64.DLLから呼び出さ
        れます。

      < FWLIB0iD64.DLL >
        FS0i-D用のライブラリです。FWLIB64.DLLから呼び出されます。

      < FWLIBNCG64.DLL >
        FS31i/32i/35i NCガイド・プロ用FOCAS2/HSSBのライブラリです。FWLIB64.DLL
        から呼び出されます。

      < FWLIB0DN64.DLL >
        FS0i-D NCガイド・プロ用FOCAS2/HSSBのライブラリです。FWLIB64.DLLから呼
        び出されます。

      < FWLIB64.LIB >
        C/C++言語において、CNC/PMCウィンドウライブラリFWLIB64.DLLとダイナミッ
        クリンクをするためのインポートライブラリです。

      < FWLIB64.H >
        CNC/PMCデータウィンドウ関数、ならびに構造体用のC/C++ヘッダファイルで
        す。
        ファイルは、機種別に分かれています。

    * その他のファイル

      < DOCUMENT\SPECE\FWLIB32.HTM >
        データウィンドウライブラリ仕様書トップページ（英語版）
        * 閲覧には、Internet Explorer 6.0以降のブラウザが必要です。

      < DOCUMENT\SPECJ\FWLIB32.HTM >
        データウィンドウライブラリ仕様書トップページ（日本語版）
        * 閲覧には、Internet Explorer 6.0以降のブラウザが必要です。

      < FWLIBPM.TXT >
        英語版Power Mateライブラリ仕様書

      < FWLIBPMJ.TXT >
        日本語版Power Mateライブラリ仕様書

      < FCA32.DLL >
        シリアル/FCA（ファナックカセットアダプタ）機器を利用するためのライブ
        ラリです。

      < FCA32.LIB >
        C/C++言語において、シリアル/FCA通信ライブラリFCA32.DLLとダイナミック
        リンクをするためのインポートライブラリです。

      < FCA32.H >
        シリアル/FCA通信関数、ならびに構造体用のC/C++ヘッダファイルです。

      < FCA32.BAS >
        シリアル/FCA通信関数、ならびに構造体用のVisual Basicヘッダファイルで
        す。

      < CONTMEM.EXE >
        CNCのメモリを表示、変更するユーティリティです。メンテナンス時に使用
        します。バックアップディスクにコピーして機械に添付してください。

      < OPTSET.EXE >
        CNCのオプションを設定するユーティリティです。メンテナンス時に使用し
        ます。バックアップディスクにコピーして機械に添付してください。

      < FWLOG.EXE >
        データウィンドウライブラリ通信ログ出力を表示するためのユーティリティで
        す。

      < LOGLEVEL.EXE >
        データウィンドウライブラリの通信ログ出力機能を制御するユーティリティで
        す。

      < NUMLOCK.EXE >
        キーボードのNumLockをON/OFFするツールです。
        このツールを使えば、フルキーボード無しで、NumLockをON/OFFできます。

      < UMSGSERV.EXE >
        CNC主導メッセージ通知機能を使用するためのPC側のサーバです。
        CNC主導メッセージ通知機能の使用方法については、データウィンドウライブ
        ラリ仕様書を参照して下さい。本機能は、ファーストイーサネット専用の機能
        です。


==========================================================================
[4] 動作環境について
==========================================================================

    本CD-ROMのソフトウェアは、Windows 95/98, Windows NT 4.0,
    Windows 2000 Professional, Windows XP Professional, Windows Vista Business,
    Windows 7 Professional, Linux(Ubuntu 12.04 LTS、Fedora 17、CentOS 6.3)用
    です。
    Windows 3.1のWin32S環境上では動作しませんのでご注意下さい。

    HSSBドライバとFOCAS2ライブラリは、Windows 7 Professional 64ビット版に対応し
    ています。

    TCP/IP用のソフトウェアをインストールする前に、「FANUCイーサネットボード
    取扱説明書(B-63354JA)」、「FANUCファストイーサネットボード/ファストデータ
    サーバ 取扱説明書(B-63644JA)」、または「FANUC 16i/18i/21i-MODEL B 結合説
    明書(機能編) (B-63523-1JA)」を参照し、以下の項目を確認してください。

      ・パソコン側のTCP/IPの設定
      ・CNC側のイーサネットボード、または組込みイーサネットの設定
      ・パソコンとCNCとのネットワーク接続の確認

    パンチプレス・レーザ関連の機能に対応しているシステムの系列／版数は次の通り
    です。
        イーサネットボード              16i/18i/21i     6561/06   以降
        ファストイーサネットボード      16i/18i/21i     6567/01   以降

    FS150iに対応しているシステムの系列／版数は次の通りです。

        イーサネットボード              15i             6563/05   以降
        ファストイーサネットボード      15i-B           6568/01   以降

    FS0i-Bに対応しているシステムの系列／版数は次の通りです。

        ファストイーサネットボード      0i-B            6567/07   以降

    FS31i/32i-A用のFOCAS2/HSSB、FOCAS2/Ethernet機能に対応しているNCガイド・プロ
    の版数は以下の通りです。

        NCガイド･プロ  (1ユーザ)       A08B-9010-J735#ZZ11  3.00版以降
        NCガイド･プロ (10ユーザ)       A08B-9010-J736#ZZ11  3.00版以降
        NCガイド･プロ (20ユーザ)       A08B-9010-J737#ZZ11  3.00版以降
        NCガイド･プロ (30ユーザ)       A08B-9010-J738#ZZ11  3.00版以降

    FS31i/32i/35i-B用のFOCAS2/HSSB、FOCAS2/Ethernet機能に対応している
    NCガイド・プロの版数は以下の通りです。

        NCガイド･プロ  (1ユーザ)       A08B-9010-J735#ZZ12  5.00版以降
        NCガイド･プロ (10ユーザ)       A08B-9010-J736#ZZ12  5.00版以降
        NCガイド･プロ (20ユーザ)       A08B-9010-J737#ZZ12  5.00版以降
        NCガイド･プロ (30ユーザ)       A08B-9010-J738#ZZ12  5.00版以降

    FS0i-D用のFOCAS2/HSSB機能に対応しているNCガイド・プロの版数は以下の通りです。

        NCガイド･プロ  (1ユーザ)       A08B-9010-J735#ZZ11  4.30版以降
        NCガイド･プロ (10ユーザ)       A08B-9010-J736#ZZ11  4.30版以降
        NCガイド･プロ (20ユーザ)       A08B-9010-J737#ZZ11  4.30版以降
        NCガイド･プロ (30ユーザ)       A08B-9010-J738#ZZ11  4.30版以降

    FS0i-D用のFOCAS2/Ethernet機能に対応しているNCガイド・プロの版数は以下の通りです。

        NCガイド･プロ  (1ユーザ)       A08B-9010-J735#ZZ11  4.00版以降
        NCガイド･プロ (10ユーザ)       A08B-9010-J736#ZZ11  4.00版以降
        NCガイド･プロ (20ユーザ)       A08B-9010-J737#ZZ11  4.00版以降
        NCガイド･プロ (30ユーザ)       A08B-9010-J738#ZZ11  4.00版以降


==========================================================================
[5] インストール方法について
==========================================================================

    ドライバのインストール手順の詳細は、README2J.TXTをお読み下さい。
    高速シリアルバス(HSSB)で接続されたパソコン、FS160i/180i/210i、FANUC PANEL i
    および、パソコン機能付CNC表示ユニット にはHSSBドライバをインストールして下
    さい。

< FWLIB32.DLL  > < FWLIBE1.DLL  > < FWLIB30i.DLL > < FWLIB150.DLL >
< FWLIB15i.DLL > < FWLIB160.DLL > < FWLIB16W.DLL > < FWLIB0i.DLL  >
< FWLIB0iB.DLL > < FWLIB0iD.DLL > < FWLIBPM.DLL  > < FWLIBPMi.DLL >
< FWLIBNCG.DLL > < FWLIB0ND.DLL >
< FWLIB64.DLL  > < FWLIBE64.DLL  > < FWLIB30i64.DLL > < FWLIB0iD64.DLL >
< FWLIBNCG64.DLL > < FWLIB0ND64.DLL >
< libfwlib32.so.1.0.0 >
    ユーザにとって都合のよいフォルダにコピーしてください。
    Windows\System (NT/2000/XP/Vista/7ではSystem32)フォルダにコピーする事を
    お勧めします。

< FWLIB32.LIB > < FWLIB32.H > < FWLIB32.BAS > < FWLIB32.CS > < FWLIB32.VB >
< FWSYMBOL.H >
< FWLIB64.LIB > < FWLIB64.H >
    開発に都合のよいフォルダにコピーしてください。

< FWPMCALM.INI >
    アプリケーションのあるフォルダかシステムフォルダにコピーしてください。

< FCA32.DLL >
    ユーザにとって都合のよいフォルダにインストールしてください。

< FCA32.LIB > < FCA32.H > < FCA32.BAS >
    開発に都合のよいフォルダにインストールしてください。


==========================================================================
[6] ソフトウェアの再配布について
==========================================================================

    本ソフトウェアを用いて開発したアプリケーションを動作させるのに必要なド
    ライバ、およびランタイムモジュールは、アプリケーションに添付して再配布
    することができます。

    具体的には、以下の拡張子のファイルを再配布することが可能です。

        *.EXE, *.DLL, *.SYS, *.VXD, *.DEF, *.INI, *.CPL, *.INF, *.CAT


==========================================================================
[7] 更新履歴
==========================================================================

    データウィンドウライブラリ仕様書(html形式)の "更新履歴" を参照して下さ
    い。

