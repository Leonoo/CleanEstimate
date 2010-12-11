# -*- coding: utf-8 -*-

# Form implementation generated from reading ui file 'ui/addwidget.ui'
#
# Created: Sat Dec 11 23:50:54 2010
#      by: PySide uic UI code generator
#
# WARNING! All changes made in this file will be lost!

from PySide import QtCore, QtGui

class Ui_AddWidget(object):
    def setupUi(self, AddWidget):
        AddWidget.setObjectName("AddWidget")
        AddWidget.resize(414, 400)
        self.horizontalLayout = QtGui.QHBoxLayout(AddWidget)
        self.horizontalLayout.setObjectName("horizontalLayout")
        self.tableView = QtGui.QTableView(AddWidget)
        self.tableView.setMaximumSize(QtCore.QSize(16777215, 16777215))
        self.tableView.setObjectName("tableView")
        self.horizontalLayout.addWidget(self.tableView)
        self.verticalLayout = QtGui.QVBoxLayout()
        self.verticalLayout.setObjectName("verticalLayout")
        self.pb_Add = QtGui.QPushButton(AddWidget)
        self.pb_Add.setObjectName("pb_Add")
        self.verticalLayout.addWidget(self.pb_Add)
        spacerItem = QtGui.QSpacerItem(20, 40, QtGui.QSizePolicy.Minimum, QtGui.QSizePolicy.Expanding)
        self.verticalLayout.addItem(spacerItem)
        self.pb_Cancel = QtGui.QPushButton(AddWidget)
        self.pb_Cancel.setObjectName("pb_Cancel")
        self.verticalLayout.addWidget(self.pb_Cancel)
        self.horizontalLayout.addLayout(self.verticalLayout)

        self.retranslateUi(AddWidget)
        QtCore.QObject.connect(self.pb_Cancel, QtCore.SIGNAL("clicked()"), AddWidget.close)
        QtCore.QMetaObject.connectSlotsByName(AddWidget)

    def retranslateUi(self, AddWidget):
        AddWidget.setWindowTitle(QtGui.QApplication.translate("AddWidget", "AddWidget", None, QtGui.QApplication.UnicodeUTF8))
        self.pb_Add.setText(QtGui.QApplication.translate("AddWidget", "Add", None, QtGui.QApplication.UnicodeUTF8))
        self.pb_Cancel.setText(QtGui.QApplication.translate("AddWidget", "Cancel", None, QtGui.QApplication.UnicodeUTF8))


if __name__ == "__main__":
    import sys
    app = QtGui.QApplication(sys.argv)
    AddWidget = QtGui.QDialog()
    ui = Ui_AddWidget()
    ui.setupUi(AddWidget)
    AddWidget.show()
    sys.exit(app.exec_())

