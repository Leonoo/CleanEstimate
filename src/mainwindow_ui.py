# -*- coding: utf-8 -*-

# Form implementation generated from reading ui file 'ui/mainwindow.ui'
#
# Created: Sat Dec 11 23:50:53 2010
#      by: PySide uic UI code generator
#
# WARNING! All changes made in this file will be lost!

from PySide import QtCore, QtGui

class Ui_MainWindow(object):
    def setupUi(self, MainWindow):
        MainWindow.setObjectName("MainWindow")
        MainWindow.resize(1020, 726)
        self.centralwidget = QtGui.QWidget(MainWindow)
        self.centralwidget.setObjectName("centralwidget")
        self.gridLayout = QtGui.QGridLayout(self.centralwidget)
        self.gridLayout.setObjectName("gridLayout")
        MainWindow.setCentralWidget(self.centralwidget)
        self.menubar = QtGui.QMenuBar(MainWindow)
        self.menubar.setGeometry(QtCore.QRect(0, 0, 1020, 20))
        self.menubar.setObjectName("menubar")
        self.menuDatei = QtGui.QMenu(self.menubar)
        self.menuDatei.setObjectName("menuDatei")
        self.menuEdit = QtGui.QMenu(self.menubar)
        self.menuEdit.setObjectName("menuEdit")
        MainWindow.setMenuBar(self.menubar)
        self.statusbar = QtGui.QStatusBar(MainWindow)
        self.statusbar.setObjectName("statusbar")
        MainWindow.setStatusBar(self.statusbar)
        self.toolBar = QtGui.QToolBar(MainWindow)
        self.toolBar.setMovable(False)
        self.toolBar.setObjectName("toolBar")
        MainWindow.addToolBar(QtCore.Qt.TopToolBarArea, self.toolBar)
        self.actionCompany = QtGui.QAction(MainWindow)
        self.actionCompany.setVisible(False)
        self.actionCompany.setObjectName("actionCompany")
        self.actionObject = QtGui.QAction(MainWindow)
        self.actionObject.setVisible(False)
        self.actionObject.setObjectName("actionObject")
        self.actionIdentifiers = QtGui.QAction(MainWindow)
        self.actionIdentifiers.setObjectName("actionIdentifiers")
        self.actionCoatings = QtGui.QAction(MainWindow)
        self.actionCoatings.setObjectName("actionCoatings")
        self.menuEdit.addAction(self.actionIdentifiers)
        self.menuEdit.addAction(self.actionCoatings)
        self.menubar.addAction(self.menuDatei.menuAction())
        self.menubar.addAction(self.menuEdit.menuAction())
        self.toolBar.addAction(self.actionCompany)
        self.toolBar.addAction(self.actionObject)

        self.retranslateUi(MainWindow)
        QtCore.QMetaObject.connectSlotsByName(MainWindow)

    def retranslateUi(self, MainWindow):
        MainWindow.setWindowTitle(QtGui.QApplication.translate("MainWindow", "CleanEstimate", None, QtGui.QApplication.UnicodeUTF8))
        self.menuDatei.setTitle(QtGui.QApplication.translate("MainWindow", "Datei", None, QtGui.QApplication.UnicodeUTF8))
        self.menuEdit.setTitle(QtGui.QApplication.translate("MainWindow", "Edit", None, QtGui.QApplication.UnicodeUTF8))
        self.toolBar.setWindowTitle(QtGui.QApplication.translate("MainWindow", "toolBar", None, QtGui.QApplication.UnicodeUTF8))
        self.actionCompany.setText(QtGui.QApplication.translate("MainWindow", "Company", None, QtGui.QApplication.UnicodeUTF8))
        self.actionObject.setText(QtGui.QApplication.translate("MainWindow", "Object", None, QtGui.QApplication.UnicodeUTF8))
        self.actionIdentifiers.setText(QtGui.QApplication.translate("MainWindow", "Identifiers", None, QtGui.QApplication.UnicodeUTF8))
        self.actionCoatings.setText(QtGui.QApplication.translate("MainWindow", "Coatings", None, QtGui.QApplication.UnicodeUTF8))


if __name__ == "__main__":
    import sys
    app = QtGui.QApplication(sys.argv)
    MainWindow = QtGui.QMainWindow()
    ui = Ui_MainWindow()
    ui.setupUi(MainWindow)
    MainWindow.show()
    sys.exit(app.exec_())

