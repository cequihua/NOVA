<?xml version="1.0" encoding="utf-16" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="no" encoding="utf-16" />

  <xsl:template match="/">
    <html xmlns="http://www.w3.org/1999/xhtml">
      <head>
      </head>
      <body style="font-family: Verdana, Tahoma, Arial; font-size: 11px;">
        <div>
          <div>
            <span style="font-size: 24px; font-weight: bold; color: #BABFA5; margin: 10px; border-bottom-style: dotted;
                border-width: 1px; border-color: #BABFA5;">Error</span>
          </div>
          <br />
          <br />
          <div>
            Mesaje: <xsl:value-of select="//message/@message" />
            <br />
            <br />
            Fuente: <xsl:value-of select="//message/@source" />
            <br />
            <br />
            Excepción: <xsl:value-of select="//message/@exception" />
            <br />
            <br />
            Detalle: <xsl:value-of select="//message/@detail" />
          </div>
        </div>
      </body>

    </html>
  </xsl:template>

</xsl:stylesheet>
