<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="/">
    <html>
      <style>
        table {
          boreder: 1px;
          font-size: 20px;
          text-align: center;
        }

        th, td {
          padding: 5px;
        }
      </style>

      <body>
        <h1>Students</h1>
        <table>
          <tr>
            <th>Name</th>
            <th>Sex</th>
            <th>Birth Date</th>
            <th>Phone</th>
            <th>Email</th>
            <th>Course</th>
            <th>Specialty</th>
            <th>Faculty Number</th>
            <th>Exams</th>
            <th>Enrollment</th>
            <th>Endorsements</th>
          </tr>
          <xsl:for-each select="/students/student">
            <tr>
              <td>
                <xsl:value-of select="name"/>
              </td>
              <td>
                <xsl:value-of select="sex"/>
              </td>
              <td>
                <xsl:value-of select="birthDate"/>
              </td>
              <td>
                <xsl:value-of select="phone"/>
              </td>
              <td>
                <xsl:value-of select="email"/>
              </td>
              <td>
                <xsl:value-of select="course"/>
              </td>
              <td>
                <xsl:value-of select="specialty"/>
              </td>
              <td>
                <xsl:value-of select="facultyNumber"/>
              </td>
              <td>
                <xsl:for-each select="exams/exam">
                  <div class="exam">
                    <strong>
                      <xsl:value-of select="examName"/>
                    </strong>
                    Tutor: <xsl:value-of select="tutor"/>
                    Score: <xsl:value-of select="score"/>
                  </div>
                </xsl:for-each>
              </td>
              <td>
                <div>
                  Date: <xsl:value-of select="enrollment/date"/>
                  Score: <xsl:value-of select="enrollment/examScore"/>
                </div>
              </td>
              <td>
                <xsl:for-each select="teacherEndorsements/teacher">
                  <div>
                    <xsl:value-of select ="teacherName"/>
                    <xsl:value-of select="endorsement"/>
                  </div>
                </xsl:for-each>
              </td>
            </tr>
          </xsl:for-each>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
