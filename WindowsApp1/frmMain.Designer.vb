<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnUpdata = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(22, 83)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(232, 55)
        Me.btnAdd.TabIndex = 0
        Me.btnAdd.Text = "Add New Passenger"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnUpdata
        '
        Me.btnUpdata.Location = New System.Drawing.Point(292, 83)
        Me.btnUpdata.Name = "btnUpdata"
        Me.btnUpdata.Size = New System.Drawing.Size(289, 55)
        Me.btnUpdata.TabIndex = 1
        Me.btnUpdata.Text = "Update Passenger Info"
        Me.btnUpdata.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(922, 83)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(235, 55)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete Passenger"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(625, 83)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(259, 55)
        Me.btnShow.TabIndex = 3
        Me.btnShow.Text = "Show All Passengers"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(1193, 83)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(203, 55)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1538, 233)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnShow)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnUpdata)
        Me.Controls.Add(Me.btnAdd)
        Me.Name = "frmMain"
        Me.Text = "Main"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnAdd As Button
    Friend WithEvents btnUpdata As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnShow As Button
    Friend WithEvents btnExit As Button
End Class
