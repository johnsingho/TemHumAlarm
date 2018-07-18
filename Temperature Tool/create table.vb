Public Class create_table

    Private Sub create_table_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim sql1 As String = "CREATE TABLE [dbo].[" + a.Text + "7weeksData] (" +
                   " [TestID]      INT            NOT NULL," +
                   " [sn]          NVARCHAR (200) NULL," +
                   " [Station]     NVARCHAR (200) NULL," +
                   " [StationType] NVARCHAR (200) NULL," +
                   " [line]        NVARCHAR (200) NULL," +
                    "[partnumber]  NVARCHAR (200) NULL," +
                   " [family]      NVARCHAR (200) NULL," +
                   " [TestTime]    DATETIME       NULL," +
                   " [IsPass]      BIT            NULL" +
                ");" +
               " CREATE INDEX [IX_" + a.Text + "7weeksData_Column] ON [dbo].[" + a.Text + "7weeksData] ([TestTime])"
        ExecuteSql(connstr, sql1)
        Dim sql2 As String = "CREATE TABLE [dbo].[" + a.Text + "7WeeksFailData] (" +
                           " [debugid]     INT            NULL," +
                           " [sn]          NVARCHAR (200) NULL," +
                           " [station]     NVARCHAR (200) NULL," +
                          "  [stationtype] NVARCHAR (200) NULL," +
                           " [line]        NVARCHAR (200) NULL," +
                           " [partnumber]  NVARCHAR (200) NULL," +
                           " [family]      NVARCHAR (200) NULL," +
                           " [testTime]    DATETIME       NULL," +
                           " [debugTime]   DATETIME       NULL," +
                           " [failcode]    NVARCHAR (200) NULL," +
                           " [defectcode]  NVARCHAR (200) NULL" +
                       " );" +
                      "  CREATE NONCLUSTERED INDEX [IX_" + a.Text + "7WeeksFailData_Column]" +
                           " ON [dbo].[" + a.Text + "7WeeksFailData]([testTime] ASC)"


        ExecuteSql(connstr, sql2)

        MsgBox("完成!")
    End Sub
End Class