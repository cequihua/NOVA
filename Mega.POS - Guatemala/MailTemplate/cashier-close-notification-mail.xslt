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
            <span style="font-size: 18px; font-weight: bold; color: #BABFA5; border-bottom-style: dotted;
                        border-width: 1px; border-color: #BABFA5;">Notificación de Cierre de Caja en MegaPOS (Con descuadre)</span>
            <img src="cid:CompanyLogo" align="right" vspace="5px" alt="Company Logo" />
          </div>
          <br />
          <br />
          <div>
            Tienda:
            <xsl:value-of select="//message/@shop" />
            <br />
            <br />
            Caja:
            <xsl:value-of select="//message/@cashier" />
            <br />
            <br />
            Número de Cierre:
            <xsl:value-of select="//message/@consecutive" />
            <br />
            <br />
            Asesor:
            <xsl:value-of select="//message/@modifiedBY" />
            <br />
            <br />
            Fecha:
            <xsl:value-of select="//message/@date" />
            <hr />
            <sub>
              Este mensaje ha sido generado automáticamente por el sistema. Por favor no
              responda a esta cuenta de correo. 
            </sub>
          </div>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
