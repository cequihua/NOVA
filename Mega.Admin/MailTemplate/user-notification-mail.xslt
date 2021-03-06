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
                        border-width: 1px; border-color: #BABFA5;">Notificación de alta o cambios a su Usuario en MegaPOS</span>
                    <img src="cid:CompanyLogo" align="right" vspace="5px" alt="Company Logo" />
                </div>
                <br />
                <br />
                <div>
                    Login:
                    <xsl:value-of select="//message/@login" />
                    <br />
                    <br />
                    Nombre:
                    <xsl:value-of select="//message/@userName" />
                    <br />
                    <br />
                    Contraseña:
                    <xsl:value-of select="//message/@password" />
                    <br />
                    <br />
                    Validez:
                    <xsl:value-of select="//message/@validDate" />
                    <br />
                    <br />
                    Roles permitidos:
                    <xsl:value-of select="//message/@roles" />
                    <br />
                    <br />
                    Tiendas permitidas:
                    <xsl:value-of select="//message/@shops" />
                    <hr />
                    <sub>Este mensaje ha sido generado automáticamente por el sistema. Por favor no
                        responda a esta cuenta de correo. Si desea contactarnos tenga el favor de hacerlo
                        a través de nuestra web o vía telefónica.</sub>
                </div>
            </div>
        </body>
        </html>
    </xsl:template>
</xsl:stylesheet>
