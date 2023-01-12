<?xml version="1.0" encoding="Shift_JIS"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:msxsl="urn:schemas-microsoft-com:xslt"
                xmlns:bop2="http://www.fanuc.co.jp/develop/mmc/bop2"
                version="1.0">


<xsl:template match="/">

<html>

  <head>
    <meta http-equiv="Content-Type" content="text/html; charset=Shift_JIS"/>
    <title><xsl:value-of select="root/func/title"/></title>
    <link rel="stylesheet" type="text/css" href="../fwlib32.css"></link>
  </head>

  <body bgcolor="#FFFFFF">
    <FONT face="ＭＳ Ｐゴシック">

    <!-- 関数名 -->
    <a name="関数名"/>
    <xsl:apply-templates select="root/func/title"/>

    <div class="text">

      <!-- 関数宣言 -->
      <a name="関数宣言"/>
      <xsl:apply-templates select="root/func/declare"/>

      <!-- 解説 -->
      <a name="解説"/>
      <xsl:apply-templates select="root/func/doc"/>

      <!-- 引数 -->
      <a name="引数"/>
      <xsl:apply-templates select="root/func/argument"/>

      <!-- 戻り値 -->
      <a name="戻り値"/>
      <xsl:apply-templates select="root/func/errcode"/>

      <!-- CNCオプション -->
      <a name="CNCオプション"/>
      <xsl:apply-templates select="root/func/option"/>

      <!-- CNCパラメータ -->
      <a name="CNCパラメータ"/>
      <xsl:apply-templates select="root/func/parameter"/>

      <!-- CNCモード -->
      <a name="CNCモード"/>
      <xsl:apply-templates select="root/func/mode"/>

      <!-- 対応機種 -->
      <a name="対応機種"/>
      <xsl:apply-templates select="root/func/support"/>

      <!-- 参照 -->
      <a name="参照"/>
      <xsl:apply-templates select="root/func/reference"/>

      <!-- プログラム例 -->
      <a name="プログラム例"/>
      <xsl:apply-templates select="root/func/example"/>

    </div>

   </FONT>

    <hr/>

  </body>

</html>

</xsl:template>





<!-- 関数名のテンプレート  -->
<xsl:template match="root/func/title">
  <p class="ftitle"><xsl:value-of select="."/></p>
</xsl:template>





<!-- 関数宣言のテンプレート  -->
<xsl:template match="root/func/declare">
  <p class="label">関数宣言</p>
  <div class="mth">
    <xsl:apply-templates select=".//vc" />
  </div>
</xsl:template>


<!-- Visual C++ 用 関数宣言のテンプレート  -->
<xsl:template match="vc">
    <xsl:choose>
        <xsl:when test="/root/func/support/m/fs0id[.!='X']">
           <xsl:value-of select="include"/> or "fwlib64.h"<p/>
        </xsl:when>

        <xsl:when test="/root/func/support/m/fs0if[.!='X']">
           <xsl:value-of select="include"/> or "fwlib64.h"<p/>
        </xsl:when>

        <xsl:when test="/root/func/support/m/fs30ia[.!='X']">
           <xsl:value-of select="include"/> or "fwlib64.h"<p/>
        </xsl:when>

        <xsl:when test="/root/func/support/m/fs30ib[.!='X']">
           <xsl:value-of select="include"/> or "fwlib64.h"<p/>
        </xsl:when>

        <xsl:when test="/root/func/support/t/fs0id[.!='X']">
           <xsl:value-of select="include"/> or "fwlib64.h"<p/>
        </xsl:when>

        <xsl:when test="/root/func/support/t/fs0if[.!='X']">
           <xsl:value-of select="include"/> or "fwlib64.h"<p/>
        </xsl:when>

        <xsl:when test="/root/func/support/t/fs30ia[.!='X']">
           <xsl:value-of select="include"/> or "fwlib64.h"<p/>
        </xsl:when>

        <xsl:when test="/root/func/support/t/fs30ib[.!='X']">
           <xsl:value-of select="include"/> or "fwlib64.h"<p/>
        </xsl:when>

        <xsl:when test="/root/func/support/p/fs0id[.!='X']">
           <xsl:value-of select="include"/> or "fwlib64.h"<p/>
        </xsl:when>

        <xsl:when test="/root/func/support/p/fs0if[.!='X']">
           <xsl:value-of select="include"/> or "fwlib64.h"<p/>
        </xsl:when>

        <xsl:when test="/root/func/support/p/fs30ib[.!='X']">
           <xsl:value-of select="include"/> or "fwlib64.h"<p/>
        </xsl:when>

        <xsl:when test="/root/func/support/l/fs30ib[.!='X']">
           <xsl:value-of select="include"/> or "fwlib64.h"<p/>
        </xsl:when>

        <xsl:when test="/root/func/support/w/fs30ia[.!='X']">
           <xsl:value-of select="include"/> or "fwlib64.h"<p/>
        </xsl:when>

        <xsl:when test="/root/func/support/w/fs30ib[.!='X']">
           <xsl:value-of select="include"/> or "fwlib64.h"<p/>
        </xsl:when>

        <xsl:when test="/root/func/support/pw/a[.!='X']">
           <xsl:value-of select="include"/> or "fwlib64.h"<p/>
        </xsl:when>

        <xsl:otherwise>
           <xsl:value-of select="include"/><p/>
        </xsl:otherwise>
    </xsl:choose>

  <xsl:copy-of select="prottype"/>
</xsl:template>


<!-- Visual Basic 用 関数宣言のテンプレート  -->
<xsl:template match="vb">
</xsl:template>





<!-- 解説のテンプレート  -->
<xsl:template match="root/func/doc">
  <xsl:if test="count(cmn) != 0">
    <p class="label">解説</p>
    <xsl:copy-of select="cmn"/>
  </xsl:if>
</xsl:template>





<!-- 引数のテンプレート  -->
<xsl:template match="root/func/argument">

  <!-- HSSB と Ethernet 接続共通の引数 -->
  <xsl:if test="count(common/*) != 0">
    <p class="label">引数</p>
    <xsl:apply-templates select="common/cmn"/>
    <xsl:apply-templates select="common/vc"/>
    <xsl:apply-templates select="common/vb"/>
  </xsl:if>

  <!-- HSSB専用 引数  -->
  <xsl:if test="count(hssb/*) != 0">
    <p class="label">引数 : HSSB</p>
    <xsl:apply-templates select="hssb/cmn"/>
    <xsl:apply-templates select="hssb/vc"/>
    <xsl:apply-templates select="hssb/vb"/>
  </xsl:if>

  <!-- Ethernet専用 引数  -->
  <xsl:if test="count(ethr/*) != 0">
  </xsl:if>

</xsl:template>


<!-- Visual C++ と Visual Basic 共通の引数のテンプレート  -->
<xsl:template match="root/func/argument/*/cmn">
  <xsl:apply-templates select="item"/>
  <xsl:copy-of select="add"/>
</xsl:template>


<!-- Visual C++ 用 引数のテンプレート  -->
<xsl:template match="root/func/argument/*/vc">
  <xsl:apply-templates select="item"/>
  <xsl:copy-of select="add"/>
</xsl:template>


<!-- Visual Basic 用 引数のテンプレート  -->
<xsl:template match="root/func/argument/*/vb">
</xsl:template>


<!-- 引数の追加  -->
<xsl:template match="root/func/argument/*/*/item">
  <div class="dtarg">
    <xsl:copy-of select="name"/>
    <xsl:if test="count(type) != 0">
      &#160;&#160;[&#160;<xsl:copy-of select="type"/>&#160;]
    </xsl:if>
  </div>
  <div class="ddarg">
    <xsl:copy-of select="content"/>
  </div>
</xsl:template>





<!-- 戻り値のテンプレート -->
<xsl:template match="root/func/errcode">
  <p class="label">戻り値</p>
  <p>成功するとEW_OKを返し、エラーの場合はEW_OK以外の値を返します。</p>
  <xsl:if test="count(item) != 0">
    <p>主なエラーは以下の通りです。</p>
    <table border="1">
      <colgroup align="center" valign="top"/>
      <colgroup align="left" valign="top"/>
      <colgroup align="left" valign="top"/>
      <tr>
        <th>戻り値</th>
        <th>意味/対処方法</th>
      </tr>
      <xsl:apply-templates select="item"/>
    </table>
  </xsl:if>

  <p><xsl:copy-of select="add"/></p>

  <p>
    <xsl:if test="count(item) != 0">
      その他の戻り値や
    </xsl:if>
    エラーの詳細については、「<a href="errcode.htm" target="_parent">データウィンドウ関数の戻り値</a>」を参照して下さい。
  </p>
</xsl:template>

<!-- エラーコード -->
<xsl:template match="item">
  <tr>
<!--<td><xsl:copy-of select="name"/></td>-->
    <xsl:choose>
        <xsl:when test="name[.='EW_PROTOCOL']">
           <td><xsl:copy-of select="name"/><br/>(-17)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_SOCKET']">
           <td><xsl:copy-of select="name"/><br/>(-16)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_NODLL']">
           <td><xsl:copy-of select="name"/><br/>(-15)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_BUS']">
           <td><xsl:copy-of select="name"/><br/>(-11)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_SYSTEM2']">
           <td><xsl:copy-of select="name"/><br/>(-10)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_HSSB']">
           <td><xsl:copy-of select="name"/><br/>(-9)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_HANDLE']">
           <td><xsl:copy-of select="name"/><br/>(-8)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_VERSION']">
           <td><xsl:copy-of select="name"/><br/>(-7)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_UNEXP']">
           <td><xsl:copy-of select="name"/><br/>(-6)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_SYSTEM']">
           <td><xsl:copy-of select="name"/><br/>(-5)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_PARITY']">
           <td><xsl:copy-of select="name"/><br/>(-4)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_MMCSYS']">
           <td><xsl:copy-of select="name"/><br/>(-3)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_RESET']">
           <td><xsl:copy-of select="name"/><br/>(-2)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_BUSY']">
           <td><xsl:copy-of select="name"/><br/>(-1)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_OK']">
           <td><xsl:copy-of select="name"/><br/>(0)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_FUNC']">
           <td><xsl:copy-of select="name"/><br/>(1)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_NOPMC']">
           <td><xsl:copy-of select="name"/><br/>(1)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_LENGTH']">
           <td><xsl:copy-of select="name"/><br/>(2)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_NUMBER']">
           <td><xsl:copy-of select="name"/><br/>(3)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_RANGE']">
           <td><xsl:copy-of select="name"/><br/>(3)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_ATTRIB']">
           <td><xsl:copy-of select="name"/><br/>(4)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_TYPE']">
           <td><xsl:copy-of select="name"/><br/>(4)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_DATA']">
           <td><xsl:copy-of select="name"/><br/>(5)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_NOOPT']">
           <td><xsl:copy-of select="name"/><br/>(6)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_PROT']">
           <td><xsl:copy-of select="name"/><br/>(7)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_OVRFLOW']">
           <td><xsl:copy-of select="name"/><br/>(8)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_PARAM']">
           <td><xsl:copy-of select="name"/><br/>(9)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_BUFFER']">
           <td><xsl:copy-of select="name"/><br/>(10)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_PATH']">
           <td><xsl:copy-of select="name"/><br/>(11)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_MODE']">
           <td><xsl:copy-of select="name"/><br/>(12)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_REJECT']">
           <td><xsl:copy-of select="name"/><br/>(13)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_DTSRVR']">
           <td><xsl:copy-of select="name"/><br/>(14)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_ALARM']">
           <td><xsl:copy-of select="name"/><br/>(15)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_STOP']">
           <td><xsl:copy-of select="name"/><br/>(16)</td>
        </xsl:when>
        <xsl:when test="name[.='EW_PASSWD']">
           <td><xsl:copy-of select="name"/><br/>(17)</td>
        </xsl:when>
        <xsl:otherwise>
           <td><xsl:copy-of select="name"/></td>
        </xsl:otherwise>
    </xsl:choose>

    <td><xsl:copy-of select="content"/></td>
  </tr>
</xsl:template>

<xsl:template match="root/func/errcode/item[@class=&quot;ois&quot;] | root/func/errcode/is/item[@class=&quot;ois&quot;]">
</xsl:template>




<!-- CNCオプションのテンプレート -->
<xsl:template match="root/func/option">
  <p class="label">CNCオプション</p>
  <p><xsl:copy-of select="cmn"/></p>
  <xsl:if test="count(need) != 0">
    <p>本機能には以下のオプションが必要です。</p>
    <ul>
      <p><xsl:copy-of select="need"/></p>
    </ul>
  </xsl:if>
  <xsl:if test="count(type) != 0">
    <p>また以下のオプションが関係します。</p>
    <ul>
      <p><xsl:copy-of select="type"/></p>
    </ul>
  </xsl:if>
  <xsl:if test="count(hssb) != 0">
    <p>HSSB接続では、</p>
    <ul>
      <p><xsl:copy-of select="hssb"/></p>
    </ul>
  </xsl:if>
  <xsl:if test="count(ethr) != 0">
  </xsl:if>
  <p><xsl:copy-of select="add"/></p>
</xsl:template>





<!-- CNCパラメータのテンプレート -->
<xsl:template match="root/func/parameter">
  <p class="label">CNCパラメータ</p>
  <xsl:if test="count(*) = 0">
    <p>本機能にパラメータは関係しません。</p>
  </xsl:if>
  <xsl:if test="count(*) != 0">
    <xsl:if test="(count(dtsv) = 0) and (count(type) = 0)">
      <p>本機能にパラメータは関係しません。</p>
    </xsl:if>
    <xsl:if test="count(dtsv) != 0">
      <p>本機能には以下のパラメータが関係します。<br/>
      詳細はDATA SERVER取扱説明書を参照して下さい。</p>
      <p><xsl:copy-of select="dtsv"/></p>
    </xsl:if>
    <xsl:if test="count(fast) != 0">
      <p>本機能には以下のパラメータが関係します。<br/>
      詳細はファストイーサネットボード取扱説明書を参照して下さい。</p>
      <p><xsl:copy-of select="fast"/></p>
    </xsl:if>
    <xsl:if test="count(type) != 0">
      <p>本機能には以下のパラメータが関係します。<br/>
      詳細は CNCのパラメータ説明書を参照して下さい。</p>
      <p><xsl:copy-of select="type"/></p>
    </xsl:if>
    <p><xsl:copy-of select="add"/></p>
  </xsl:if>
</xsl:template>





<!-- CNCモードのテンプレート -->
<xsl:template match="root/func/mode">
  <p class="label">CNCモード</p>
  <xsl:if test="count(*) = 0">
    <p>すべてのモードで使用できます。</p>
  </xsl:if>
  <xsl:if test="count(*) != 0">
    <p><xsl:copy-of select="cmn"/></p>
    <p><xsl:copy-of select="add"/></p>
  </xsl:if>
</xsl:template>





<!-- 対応機種のテンプレート -->
<xsl:template match="root/func/support">
  <p class="label">対応機種</p>
  <table border="1">
    <colgroup width="100em"/>
    <colgroup align="center" width="40em"/>
    <colgroup align="center" width="40em"/>
    <colgroup align="center" width="40em"/>
    <colgroup align="center" width="40em"/>
    <colgroup align="center" width="40em"/>
    <colgroup align="center" width="40em"/>
    <colgroup align="center" width="40em"/>
    <colgroup align="center" width="40em"/>
    <colgroup align="center" width="40em"/>
    <colgroup align="center" width="40em"/>
    <colgroup align="center" width="40em"/>
    <colgroup align="center" width="40em"/>
    <colgroup align="center" width="40em"/>
    <colgroup align="center" width="40em"/>
    <colgroup align="center" width="40em"/>
    <colgroup align="center" width="40em"/>
    <colgroup align="center" width="40em"/>
    <colgroup align="center" width="40em"/>
  <tr>
    <th></th>
    <th>0i-A</th>
    <th>0i-B/C(注)</th>
    <th>0i-D</th>
    <th>0i-F</th>
    <th>15</th>
    <th>15i</th>
    <th>16</th>
    <th>18</th>
    <th>21</th>
    <th>16i-A</th>
    <th>18i-A</th>
    <th>21i-A</th>
    <th>16i-B</th>
    <th>18i-B</th>
    <th>21i-B</th>
    <th>30i-A</th>
    <th>30i-B</th>
  </tr>
  <tr><td>M (マシニング) </td><xsl:apply-templates select="m"/></tr>
  <tr><td>T (旋盤)       </td><xsl:apply-templates select="t"/></tr>
  <tr><td>LC (ローダ制御)</td><xsl:apply-templates select="lc"/></tr>
  </table><p/>

  <table border="1">
    <colgroup width="100em"/>
    <colgroup align="center" width="40em"/>
    <colgroup align="center" width="40em"/>
    <colgroup align="center" width="40em"/>
    <colgroup align="center" width="40em"/>
    <colgroup align="center" width="40em"/>
    <colgroup align="center" width="40em"/>
    <colgroup align="center" width="40em"/>
  <tr>
    <th></th><th>0i-D</th><th>0i-F</th><th>16i</th><th>18i</th><th>30i-A</th><th>30i-B</th>
  </tr>
  <tr><td>P (パンチプレス)</td><xsl:apply-templates select="p"/></tr>
  <tr><td>L (レーザ)      </td><xsl:apply-templates select="l"/></tr>
  <tr><td>W (ワイヤカット)</td><xsl:apply-templates select="w"/></tr>
  </table><p/>

  <br/>
  <table border="1">
    <colgroup width="100em"/>
    <colgroup align="center" width="86em"/>
    <colgroup align="center" width="86em"/>
<!--  <tr>
    <th></th><th>Power Mate i</th>
  </tr> -->
  <tr><td>Power Mate i-D</td><xsl:apply-templates select="pw/d"/></tr>
  <tr><td>Power Mate i-H</td><xsl:apply-templates select="pw/h"/></tr>
  <tr><td>Power Motion i-A</td><xsl:apply-templates select="pw/a"/></tr>
  </table><p/>
  
  <xsl:copy-of select="original"/>

  <p>※&#160;"O"：Ethernet接続とHSSB接続の両方に対応、"E"：Ethernet接続のみ対応、"H"：HSSB接続のみ対応、"X"：未対応、"-"：なし</p>
  <p style="background-color:mistyrose;">注) 0i-Cは、HSSB機能をサポートしません。</p>
  <p><xsl:copy-of select="add"/></p>
</xsl:template>


<xsl:template match="m">
  <xsl:call-template name="support"> <xsl:with-param name="item" select="fs0ia"/>  </xsl:call-template>
  <xsl:call-template name="support"> <xsl:with-param name="item" select="fs0ib"/>  </xsl:call-template>
  <xsl:call-template name="support"> <xsl:with-param name="item" select="fs0id"/>  </xsl:call-template>
  <xsl:call-template name="support"> <xsl:with-param name="item" select="fs0if"/>  </xsl:call-template>
  <xsl:call-template name="support"> <xsl:with-param name="item" select="fs15"/>   </xsl:call-template>
  <xsl:call-template name="support"> <xsl:with-param name="item" select="fs15i"/>  </xsl:call-template>
  <xsl:call-template name="support"> <xsl:with-param name="item" select="fs16"/>   </xsl:call-template>
  <xsl:call-template name="support"> <xsl:with-param name="item" select="fs18"/>   </xsl:call-template>
  <xsl:call-template name="support"> <xsl:with-param name="item" select="fs21"/>   </xsl:call-template>
  <xsl:call-template name="support"> <xsl:with-param name="item" select="fs16ia"/> </xsl:call-template>
  <xsl:call-template name="support"> <xsl:with-param name="item" select="fs18ia"/> </xsl:call-template>
  <xsl:call-template name="support"> <xsl:with-param name="item" select="fs21ia"/> </xsl:call-template>
  <xsl:call-template name="support"> <xsl:with-param name="item" select="fs16ib"/> </xsl:call-template>
  <xsl:call-template name="support"> <xsl:with-param name="item" select="fs18ib"/> </xsl:call-template>
  <xsl:call-template name="support"> <xsl:with-param name="item" select="fs21ib"/> </xsl:call-template>
  <xsl:call-template name="support"> <xsl:with-param name="item" select="fs30ia"/> </xsl:call-template>
  <xsl:call-template name="support"> <xsl:with-param name="item" select="fs30ib"/> </xsl:call-template>
</xsl:template>

<xsl:template match="t">
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs0ia"/>  </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs0ib"/>  </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs0id"/>  </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs0if"/>  </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs15"/>   </xsl:call-template>
  <xsl:call-template name="support2"> <xsl:with-param name="item" select="fs15i"/>  </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs16"/>   </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs18"/>   </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs21"/>   </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs16ia"/> </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs18ia"/> </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs21ia"/> </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs16ib"/> </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs18ib"/> </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs21ib"/> </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs30ia"/> </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs30ib"/> </xsl:call-template>
</xsl:template>

<xsl:template match="lc">
  <xsl:call-template name="support2"> <xsl:with-param name="item" select="fs0ia"/>  </xsl:call-template>
  <xsl:call-template name="support2"> <xsl:with-param name="item" select="fs0ib"/>  </xsl:call-template>
  <xsl:call-template name="support2"> <xsl:with-param name="item" select="fs0id"/>  </xsl:call-template>
  <xsl:call-template name="support2"> <xsl:with-param name="item" select="fs0if"/>  </xsl:call-template>
  <xsl:call-template name="support2"> <xsl:with-param name="item" select="fs15"/>   </xsl:call-template>
  <xsl:call-template name="support2"> <xsl:with-param name="item" select="fs15i"/>  </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs16"/>   </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs18"/>   </xsl:call-template>
  <xsl:call-template name="support2"> <xsl:with-param name="item" select="fs21"/>   </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs16ia"/> </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs18ia"/> </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs21ia"/> </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs16ib"/> </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs18ib"/> </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs21ib"/> </xsl:call-template>
  <xsl:call-template name="support2"> <xsl:with-param name="item" select="fs30ia"/> </xsl:call-template>
  <xsl:call-template name="support2"> <xsl:with-param name="item" select="fs30ib"/> </xsl:call-template>
</xsl:template>

<xsl:template match="p">
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs0id"/>   </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs0if"/>   </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs16i"/>   </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs18i"/>   </xsl:call-template>
  <xsl:call-template name="support2"> <xsl:with-param name="item" select="fs30ia"/>  </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs30ib"/>  </xsl:call-template>
</xsl:template>

<xsl:template match="l">
  <xsl:call-template name="support2"> <xsl:with-param name="item" select="fs0id"/>  </xsl:call-template>
  <xsl:call-template name="support2"> <xsl:with-param name="item" select="fs0if"/>  </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs16i"/>  </xsl:call-template>
  <xsl:call-template name="support2"> <xsl:with-param name="item" select="fs18i"/>  </xsl:call-template>
  <xsl:call-template name="support2"> <xsl:with-param name="item" select="fs30ia"/> </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs30ib"/> </xsl:call-template>
</xsl:template>

<xsl:template match="w">
  <xsl:call-template name="support2"> <xsl:with-param name="item" select="fs0id"/>   </xsl:call-template>
  <xsl:call-template name="support2"> <xsl:with-param name="item" select="fs0if"/>   </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs16i"/>   </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs18i"/>   </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs30ia"/>  </xsl:call-template>
  <xsl:call-template name="support">  <xsl:with-param name="item" select="fs30ib"/>  </xsl:call-template>
</xsl:template>

<xsl:template match="pw/d | pw/h | pw/a">
  <xsl:call-template name="support"> <xsl:with-param name="item" select="."/>      </xsl:call-template>
</xsl:template>


<xsl:template name="support">
  <xsl:param name="item"/>
  <td>
  <xsl:attribute name="class">
    <xsl:choose>
      <xsl:when test="$item[. ='O']">support</xsl:when>
      <xsl:when test="$item[. ='E']">support</xsl:when>
      <xsl:when test="$item[. ='H']">support</xsl:when>
    </xsl:choose>
  </xsl:attribute>
  <xsl:copy-of select="$item"/>
  </td>
</xsl:template>

<xsl:template name="support2">
  <xsl:param name="item"/>
  <td>
  -
  </td>
</xsl:template>



<!-- 参照のテンプレート -->
<xsl:template match="root/func/reference">
  <xsl:if test="count(*) != 0"> 
    <p class="label">参照</p>
    <p><xsl:apply-templates select="item"/></p>
  </xsl:if>
</xsl:template>


<xsl:template match="root/func/reference/item">
  <a class="func" target="_parent">
    <xsl:attribute name="href"><xsl:value-of select="file"/></xsl:attribute>
    <xsl:value-of select="name"/>
  </a>&#160;&#160;
</xsl:template>



<!-- プログラム例のテンプレート -->
<xsl:template match="root/func/example">
  <xsl:if test="count(*) != 0">
    <p class="label">プログラム例</p>
    <p><xsl:copy-of select="common"/></p>
    <pre><xsl:value-of select="vc"/></pre>
  </xsl:if>
</xsl:template>



</xsl:stylesheet>
