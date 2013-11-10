module Tff.WinForms

open System.Drawing
open System.Windows.Forms
 
let form = new Form(Width = 400, Height = 100)
let font = new Font("Times New Roman", 28.0f)
let label = new Label(Dock=DockStyle.Fill, Font=font,
                        TextAlign=ContentAlignment.MiddleCenter,
                        Text="Hello User Group!")
 
do form.Controls.Add(label)
 
form.Show()
