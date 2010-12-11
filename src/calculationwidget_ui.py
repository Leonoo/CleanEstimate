# -*- coding: utf-8 -*-

# Form implementation generated from reading ui file 'ui/calculationwidget.ui'
#
# Created: Sat Dec 11 23:50:54 2010
#      by: PySide uic UI code generator
#
# WARNING! All changes made in this file will be lost!

from PySide import QtCore, QtGui

class Ui_CalculationWidget(object):
    def setupUi(self, CalculationWidget):
        CalculationWidget.setObjectName("CalculationWidget")
        CalculationWidget.resize(762, 558)
        self.horizontalLayout = QtGui.QHBoxLayout(CalculationWidget)
        self.horizontalLayout.setObjectName("horizontalLayout")
        self.tableView = QtGui.QTableView(CalculationWidget)
        self.tableView.setMaximumSize(QtCore.QSize(16777215, 16777215))
        self.tableView.setObjectName("tableView")
        self.horizontalLayout.addWidget(self.tableView)

        self.retranslateUi(CalculationWidget)
        QtCore.QMetaObject.connectSlotsByName(CalculationWidget)

    def retranslateUi(self, CalculationWidget):
        CalculationWidget.setWindowTitle(QtGui.QApplication.translate("CalculationWidget", "CalculationWidget", None, QtGui.QApplication.UnicodeUTF8))


if __name__ == "__main__":
    import sys
    app = QtGui.QApplication(sys.argv)
    CalculationWidget = QtGui.QWidget()
    ui = Ui_CalculationWidget()
    ui.setupUi(CalculationWidget)
    CalculationWidget.show()
    sys.exit(app.exec_())

