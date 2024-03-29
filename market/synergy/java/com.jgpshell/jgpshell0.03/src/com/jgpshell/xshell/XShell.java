// $Id$

/**
 * Author : Moez Ben MBarka Moez
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

/*
 * XShell.java
 *
 * Created on April 15, 2007, 1:14 PM
 */

package com.jgpshell.xshell;

import com.jgpshell.shell.*;

/**
 *
 * @author  Moez Ben MBarka
 * @version $Revision$
 */
public class XShell extends javax.swing.JFrame {
    
    public static JGPShell jgpshell = null;
    private static Screen screen = null;
    private static Command command = null;
    private static Session session = null;
    
    /** Creates new form XShell */
    public XShell() {
        initComponents();
    }
    
    /** This method is called from within the constructor to
     * initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is
     * always regenerated by the Form Editor.
     */
    private void initComponents() {//GEN-BEGIN:initComponents
        jScrollPane1 = new javax.swing.JScrollPane();
        shell = new javax.swing.JTextArea();
        cmd = new javax.swing.JTextField();
        run = new javax.swing.JButton();

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);
        setTitle("JGPShell Client tool v0.1");
        addWindowListener(new java.awt.event.WindowAdapter() {
            public void windowOpened(java.awt.event.WindowEvent evt) {
                init_client(evt);
            }
        });

        shell.setColumns(20);
        shell.setRows(5);
        jScrollPane1.setViewportView(shell);

        cmd.addKeyListener(new java.awt.event.KeyAdapter() {
            public void keyPressed(java.awt.event.KeyEvent evt) {
                submit_cmd(evt);
            }
        });

        run.setText("run");
        run.setActionCommand("run");
        run.addMouseListener(new java.awt.event.MouseAdapter() {
            public void mouseClicked(java.awt.event.MouseEvent evt) {
                execute_cmd(evt);
            }
        });

        org.jdesktop.layout.GroupLayout layout = new org.jdesktop.layout.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(org.jdesktop.layout.GroupLayout.LEADING)
            .add(layout.createSequentialGroup()
                .addContainerGap()
                .add(layout.createParallelGroup(org.jdesktop.layout.GroupLayout.LEADING)
                    .add(layout.createSequentialGroup()
                        .add(cmd, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, 474, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED, 15, Short.MAX_VALUE)
                        .add(run))
                    .add(jScrollPane1, org.jdesktop.layout.GroupLayout.DEFAULT_SIZE, 544, Short.MAX_VALUE))
                .addContainerGap())
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(org.jdesktop.layout.GroupLayout.LEADING)
            .add(layout.createSequentialGroup()
                .add(jScrollPane1, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, 303, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                .add(layout.createParallelGroup(org.jdesktop.layout.GroupLayout.BASELINE)
                    .add(cmd, org.jdesktop.layout.GroupLayout.DEFAULT_SIZE, 32, Short.MAX_VALUE)
                    .add(run)))
        );
        pack();
    }//GEN-END:initComponents

    private void submit_cmd(java.awt.event.KeyEvent evt) {//GEN-FIRST:event_submit_cmd
         if (evt.getKeyChar() == '\n') {
            execute_cmd();
        }
    }//GEN-LAST:event_submit_cmd

    private void init_client(java.awt.event.WindowEvent evt) {//GEN-FIRST:event_init_client
         init_shell();
        cmd.grabFocus();
    }//GEN-LAST:event_init_client

    private void execute_cmd(java.awt.event.MouseEvent evt) {//GEN-FIRST:event_execute_cmd
        execute_cmd();
    }//GEN-LAST:event_execute_cmd
    
    /**
     * @param args the command line arguments
     */
    public static void main(String args[]) {
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                new XShell().setVisible(true);
            }
        });
    }                       
    
    private void execute_cmd() {
        screen.writeln(cmd.getText());
        command.execute_cmd(jgpshell, cmd.getText());
        cmd.setText(null);
    }
    
    private void init_shell() {
        screen= new Screen(shell);
        session = new Session() ;
        command = new Command(screen, session) ;
        jgpshell = new JGPShell(command) ;
        session.set_screen(screen) ;
        session.init() ;
    }
    
    // Variables declaration - do not modify//GEN-BEGIN:variables
    javax.swing.JTextField cmd;
    javax.swing.JScrollPane jScrollPane1;
    javax.swing.JButton run;
    javax.swing.JTextArea shell;
    // End of variables declaration//GEN-END:variables
    
}


