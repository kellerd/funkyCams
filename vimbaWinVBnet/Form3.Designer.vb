﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form3
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(16, 39)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(112, 38)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "GigE Camera"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(168, 39)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(112, 38)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "FoculusCamera"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(320, 39)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(112, 38)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "Basler Camera"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(475, 39)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(112, 38)
        Me.Button4.TabIndex = 3
        Me.Button4.Text = "SVS Vistek Camera"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(16, 115)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(112, 38)
        Me.Button5.TabIndex = 4
        Me.Button5.Text = "GigE Astro Camera"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(475, 115)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(112, 38)
        Me.Button6.TabIndex = 5
        Me.Button6.Text = "Baumer Gige Camera"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Form3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(643, 188)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form3"
        Me.Text = "sky watcher"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
End Class
