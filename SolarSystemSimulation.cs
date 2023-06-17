using System;
using System.Drawing;
using System.Windows.Forms;

public class Form1 : Form
{
    private float earthAngle = 0f;
    private float moonAngle = 0f;
    private Timer timer;

    public Form1()
    {
        this.Text = "Solar System Simulation";
        this.ClientSize = new Size(800, 600);

        timer = new Timer();
        timer.Interval = 50; // Aktualisierung alle 50 Millisekunden
        timer.Tick += Timer_Tick;
        timer.Start();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        // Aktualisiere die Positionen der Himmelskörper
        earthAngle += 1f;
        moonAngle += 5f;

        this.Invalidate(); // Erzwinge das Neuzeichnen des Formulars
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Form_Paint(this, e);
    }

    private void Form_Paint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        g.Clear(Color.Black); // Setze den Hintergrund auf Schwarz

        // Betrachterposition (in diesem Fall der Ursprung)
        float viewerX = this.ClientSize.Width / 10f;
        float viewerY = this.ClientSize.Height / 8f;

        // Position der Sonne relativ zum Betrachter
        float sunX = viewerX;
        float sunY = viewerY - 100f;
        float sunZ = 0f;

        // Position der Erde relativ zum Betrachter
        float earthOrbitRadius = 200f;
        float earthX = viewerX + earthOrbitRadius * (float)Math.Cos(Math.PI * earthAngle / 180f);
        float earthY = viewerY - earthOrbitRadius * (float)Math.Sin(Math.PI * earthAngle / 180f);
        float earthZ = -50f;

        // Position des Mondes relativ zum Betrachter
        float moonOrbitRadius = 50f;
        float moonX = earthX + moonOrbitRadius * (float)Math.Cos(Math.PI * moonAngle / 180f);
        float moonY = earthY - moonOrbitRadius * (float)Math.Sin(Math.PI * moonAngle / 180f);
        float moonZ = -20f;

        // Zeichne die Himmelskörper
        DrawSphere(g, Brushes.Yellow, sunX, sunY, sunZ, 50);
        DrawSphere(g, Brushes.Blue, earthX, earthY, earthZ, 20);
        DrawSphere(g, Brushes.Gray, moonX, moonY, moonZ, 10);
    }

    private void DrawSphere(Graphics g, Brush brush, float x, float y, float z, float radius)
    {
        // Berechne die Perspektivtransformation
        float scale = 200f / (200f + z); // Einstellbarer Skalierungsfaktor für die Perspektive

        // Wende die Perspektivtransformation auf die Koordinaten an
        float drawX = x * scale + this.ClientSize.Width / 2f;
        float drawY = y * scale + this.ClientSize.Height / 2f;
        float drawRadius = radius * scale;

        // Zeichne den Kreis (Ellipse) auf dem Formular
        float diameter = 2f * drawRadius;
        g.FillEllipse(brush, drawX - drawRadius, drawY - drawRadius, diameter, diameter);
    }
}

