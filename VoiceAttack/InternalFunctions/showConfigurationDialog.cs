using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;

public class VAInline
{
    public void main()
    {
        Form frm = new MainSettingsForm(VA);
        Application.Run(frm);
    }
}



public class MainSettingsForm : Form
{
    private Guid writeSettingsToFileGuid;
    private Guid reloadProfileGuid;
    private VoiceAttack.VoiceAttackInvokeProxyClass VA;
    public ComboBox activeShipNameComboBox = null;
    public ComboBox shipSelectionComboBox = null;
    private CheckBox galaxapediaEnabledOption = null;
    private CheckBox constellationsEnabledOption = null;
    private CheckBox quantumTheoryEnabledOption = null;
    private Button editShipBtn;
    private Button deleteShipBtn;
    public List<string> shipList;
    public List<string> wgNameList;
    public bool changesMade;
    public bool reloadRequired;
    public bool soundFilesChanged;


    public MainSettingsForm(VoiceAttack.VoiceAttackInvokeProxyClass vap)
    {
        VA = vap;
        changesMade = false;
        reloadRequired = false;
        soundFilesChanged = false;

        string shipListStr = VA.GetText(">>shipNameListStr");
        shipList = new List<string>(shipListStr.Split(';'));
        shipList.Sort();

        string wgNameListStr = VA.GetText(">>weaponGroupListStr");
        wgNameList = new List<string>(wgNameListStr.Split(';'));
        wgNameList.Sort();

        string tmpCmdId = VA.GetText(">writeSettingsToFileCommandId");
        if (!string.IsNullOrEmpty(tmpCmdId))  writeSettingsToFileGuid = new Guid(tmpCmdId);

        tmpCmdId = VA.GetText(">reloadProfileCommandId");
        if (!string.IsNullOrEmpty(tmpCmdId))  reloadProfileGuid = new Guid(tmpCmdId);

        InitializeComponent();
    }


    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }


    private void constellationsEnabled_CheckStateChanged(Object sender, EventArgs e)
    {
        bool isChecked = ((CheckBox)sender).CheckState == CheckState.Checked;
        VA.SetBoolean(">>constellationsEnabled", isChecked);

        changesMade = true;
        reloadRequired = true;
    }
    private void galaxapediaEnabled_CheckStateChanged(Object sender, EventArgs e)
    {
        bool isChecked = ((CheckBox)sender).CheckState == CheckState.Checked;
        VA.SetBoolean(">>galaxapediaEnabled", isChecked);

        changesMade = true;
        reloadRequired = true;
    }
    private void quantumTheoryEnabled_CheckStateChanged(Object sender, EventArgs e)
    {
        bool isChecked = ((CheckBox)sender).CheckState == CheckState.Checked;
        VA.SetBoolean(">>quantumTheoryEnabled", isChecked);

        changesMade = true;
        reloadRequired = true;
    }
    private void codexEnabled_CheckStateChanged(Object sender, EventArgs e)
    {
        bool isChecked = ((CheckBox)sender).CheckState == CheckState.Checked;
        VA.SetBoolean(">>codexEnabled", isChecked);

        changesMade = true;
        reloadRequired = true;
    }
    private void gameVoiceEnabled_CheckStateChanged(Object sender, EventArgs e)
    {
        bool isChecked = ((CheckBox)sender).CheckState == CheckState.Checked;
        VA.SetBoolean(">>gameVoiceEnabled", isChecked);

        changesMade = true;
    }
    private void gameVoiceActionsQuiet_CheckStateChanged(Object sender, EventArgs e)
    {
        bool isChecked = ((CheckBox)sender).CheckState == CheckState.Checked;
        VA.SetBoolean(">>gameVoiceActionsQuiet", isChecked);

        changesMade = true;
    }
    private void headphonesInUse_CheckStateChanged(Object sender, EventArgs e)
    {
        bool isChecked = ((CheckBox)sender).CheckState == CheckState.Checked;
        VA.SetBoolean(">>headphonesInUse", isChecked);

        changesMade = true;
    }
    private void title_SelectedValueChanged(Object sender, EventArgs e)
    {
        string value = ((ComboBox)sender).SelectedItem.ToString();
        VA.SetText(">>title", value);

        changesMade = true;
    }
    private void companionName_SelectedValueChanged(Object sender, EventArgs e)
    {
        string value = ((ComboBox)sender).SelectedItem.ToString();
        VA.SetText(">>companionName", value);
        VA.SetText(">>voiceDir", "sf-i_" + value);

        string realFilePath = VA.ParseTokens(@"{VA_SOUNDS}\{TXT:>>voiceDir}\");
        string[] files;

        if (Directory.Exists(realFilePath + "Constellations")) {
            files = Directory.GetFiles(realFilePath + "Constellations", "*.mp3");
            if (files.Length > 0) {
                constellationsEnabledOption.Enabled = true;
                if (VA.GetBoolean(">>constellationsEnabled") == true)
                    constellationsEnabledOption.Checked = true;
                else
                    constellationsEnabledOption.Checked = false;
            } else {
                constellationsEnabledOption.Enabled = false;
            }
        } else {
            constellationsEnabledOption.Enabled = false;
        }

        if (Directory.Exists(realFilePath + "Quantum Theory")) {
            files = Directory.GetFiles(realFilePath + "Quantum Theory", "*.mp3");
            if (files.Length > 0) {
                quantumTheoryEnabledOption.Enabled = true;
                if (VA.GetBoolean(">>quantumTheoryEnabled") == true)
                    quantumTheoryEnabledOption.Checked = true;
                else
                    quantumTheoryEnabledOption.Checked = false;
            } else {
                quantumTheoryEnabledOption.Enabled = false;
            }
        } else {
            quantumTheoryEnabledOption.Enabled = false;
        }

        if (Directory.Exists(realFilePath + "Galaxapedia")) {
            files = Directory.GetFiles(realFilePath + "Galaxapedia", "*.mp3");
            if (files.Length > 0) {
                galaxapediaEnabledOption.Enabled = true;
                if (VA.GetBoolean(">>galaxapediaEnabled") == true)
                    galaxapediaEnabledOption.Checked = true;
                else
                    galaxapediaEnabledOption.Checked = false;
            } else {
                galaxapediaEnabledOption.Enabled = false;
            }
        } else {
            galaxapediaEnabledOption.Enabled = false;
        }

        changesMade = true;
        reloadRequired = true;
    }
    private void activeShipName_SelectedValueChanged(Object sender, EventArgs e)
    {
        string value = ((ComboBox)sender).SelectedItem.ToString();
        VA.SetText(">>activeShipName", value);

        changesMade = true;
        reloadRequired = true;
    }


    private void MainSettingsForm_Closing(Object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (changesMade) {
            writeSettings();
        }
        if (reloadRequired || soundFilesChanged) {
            reloadProfile();
        }
    }
    private void closeBtn_Click(object sender, EventArgs e)
    {
        this.Close();
    }


    private void editShipBtn_Click(object sender, EventArgs e)
    {
        string shipName = shipSelectionComboBox.Text.Trim();

        EditShipForm form = new EditShipForm(shipName, VA, this);
        form.ShowDialog();
    }

    private void deleteShipBtn_Click(object sender, EventArgs e)
    {
        string shipName = shipSelectionComboBox.Text.Trim();

        if (!String.IsNullOrEmpty(shipName) && shipList.Contains(shipName)) {
            if (MessageBox.Show("Are you sure you want to delete " + shipName + " ship?", "My Application", MessageBoxButtons.YesNo) ==  DialogResult.Yes) {
                shipSelectionComboBox.Items.Remove(shipName);
                activeShipNameComboBox.Items.Remove(shipName);

                //*** Set the ship to be no longer in use
                VA.SetBoolean(">>shipInfo[" + shipName + "].isInUse", false);

                foreach (string wgName in wgNameList) {
                    string tmpVarName = ">>shipInfo[" + shipName + "].weaponGroup[" + wgName + "]";
                    string settingName = tmpVarName + ".isActive";
                    VA.SetBoolean(settingName, false);

                    settingName = tmpVarName + ".weaponKeyPress.len";
                    VA.SetInt(settingName, 0);
                }

                shipList.Remove(shipName);
                if (VA.GetText(">>activeShipName") == shipName) {
                    activeShipNameComboBox.SelectedItem = shipList[0];
                    VA.SetText(">>activeShipName", shipList[0]);
                }

                changesMade = true;
                reloadRequired = true;
            }
        }
    }

/*
    public void debugBtn_Click(object sender, EventArgs e)
    {
        string message = "";
        List<string> allKeys = new List<string>();

        string[] stringKeyList = vaProxy._vaProxyStringDict.Keys.ToArray();
        string[] boolKeyList = vaProxy._vaProxyBooleanDict.Keys.ToArray();
        string[] intKeyList = vaProxy._vaProxyIntDict.Keys.ToArray();
        string[] decKeyList = vaProxy._vaProxyDecDict.Keys.ToArray();

        allKeys.AddRange(stringKeyList);
        allKeys.AddRange(boolKeyList);
        allKeys.AddRange(intKeyList);
        allKeys.AddRange(decKeyList);
        allKeys.Sort();

        foreach (string sname in allKeys) {
            if (Array.Exists(stringKeyList, element => element == sname)) {
                message += sname + " = " + vaProxy._vaProxyStringDict[sname] + "\n";
            } else if (Array.Exists(boolKeyList, element => element == sname)) {
                message += sname + " = " + vaProxy._vaProxyBooleanDict[sname] + "\n";
            } else if (Array.Exists(intKeyList, element => element == sname)) {
                message += sname + " = " + vaProxy._vaProxyIntDict[sname] + "\n";
            } else if (Array.Exists(decKeyList, element => element == sname)) {
                message += sname + " = " + vaProxy._vaProxyDecDict[sname] + "\n";
            }
        }

        FlexibleMessageBox.Show(message);
    }
*/

    private void shipSelection_TextChanged(object sender, EventArgs e)
    {
        string selShipName = shipSelectionComboBox.Text.Trim();
        if (!shipList.Contains(selShipName)) {
            editShipBtn.Text = "Add";
            deleteShipBtn.Hide();
        } else {
            editShipBtn.Text = "Edit";
            deleteShipBtn.Show();
        }
    }

    private void dynamicTableLayoutPanel_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
    {
        if (e.Row == 7)
            e.Graphics.DrawLine(Pens.Black, e.CellBounds.Location, new Point(e.CellBounds.Right, e.CellBounds.Top));
    }

    private void InitializeComponent()
    {
        this.SuspendLayout();


        TableLayoutPanel dynamicTableLayoutPanel = new TableLayoutPanel();

        dynamicTableLayoutPanel.Location = new Point(10, 10);
        dynamicTableLayoutPanel.Name = "OuterLayout";
        dynamicTableLayoutPanel.Size = new Size(480, 400);
        dynamicTableLayoutPanel.TabIndex = 0;
        dynamicTableLayoutPanel.ColumnCount = 4;
        dynamicTableLayoutPanel.RowCount = 16;
        dynamicTableLayoutPanel.BorderStyle = BorderStyle.FixedSingle;
        //*** remove following line after setup complete
        // dynamicTableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
        dynamicTableLayoutPanel.Padding = new Padding(10);

        dynamicTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize, 0));
        dynamicTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize, 0));
        dynamicTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize, 0));
        dynamicTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90));
        dynamicTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));
        dynamicTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));
        dynamicTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));
        dynamicTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));
        dynamicTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));
        dynamicTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));
        dynamicTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));
        dynamicTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));
        dynamicTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 5));
        dynamicTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));
        dynamicTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));
        dynamicTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));
        dynamicTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));
        dynamicTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));
        dynamicTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 99));
        dynamicTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));

        dynamicTableLayoutPanel.CellPaint += dynamicTableLayoutPanel_CellPaint;

        int curRow = 0;

        //*** title Setting ***
        /*
        string title = VA.GetText(">>title");

        Label titleOptionText = new Label();
        titleOptionText.Text = "Your Preferred Title";
        titleOptionText.Size = new Size(titleOptionText.PreferredWidth, titleOptionText.PreferredHeight);
        titleOptionText.TextAlign = ContentAlignment.MiddleRight;
        titleOptionText.Anchor = AnchorStyles.Right;
        dynamicTableLayoutPanel.Controls.Add(titleOptionText, 0, 0);

        List<String> titleList = new List<String>();
        titleList.Add("Commander");
        titleList.Add("Captain");

        ComboBox titleOption = new ComboBox();
        titleOption.DropDownStyle = ComboBoxStyle.DropDownList;
        titleOption.Items.AddRange(titleList.ToArray());
        if (!String.IsNullOrEmpty(title) && titleList.Contains(title)) {
            titleOption.SelectedItem = title;
        }
        titleOption.SelectedValueChanged += new System.EventHandler(title_SelectedValueChanged);
        dynamicTableLayoutPanel.Controls.Add(titleOption, 1, 0);
        */

        //*** companionName Setting ***
        string companionName = VA.GetText(">>companionName");

        Label companionNameOptionText = new Label();
        companionNameOptionText.Text = "Selected Companion";
        companionNameOptionText.Size = new Size(companionNameOptionText.PreferredWidth, companionNameOptionText.PreferredHeight);
        companionNameOptionText.TextAlign = ContentAlignment.MiddleRight;
        companionNameOptionText.Anchor = AnchorStyles.Right;
        dynamicTableLayoutPanel.Controls.Add(companionNameOptionText, 0, curRow);

        string companionNameListStr = VA.GetText(">>companionNameList");
        string[] companionNameList = { };
        if (!String.IsNullOrEmpty(companionNameListStr)) {
            companionNameList = companionNameListStr.Split(';');
            Array.Sort(companionNameList);
        }

        ComboBox companionNameOption = new ComboBox();
        companionNameOption.DropDownStyle = ComboBoxStyle.DropDownList;
        companionNameOption.Items.AddRange(companionNameList);
        if (!String.IsNullOrEmpty(companionName) && companionNameList.Contains(companionName)) {
            companionNameOption.SelectedItem = companionName;
        }
        companionNameOption.SelectedValueChanged += new System.EventHandler(companionName_SelectedValueChanged);
        dynamicTableLayoutPanel.Controls.Add(companionNameOption, 1, curRow);
        dynamicTableLayoutPanel.SetColumnSpan(companionNameOption, 2);
        curRow++;

        //*** galaxapediaEnabled Setting ***
        bool? galaxapediaEnabled = VA.GetBoolean(">>galaxapediaEnabled");
        Label galaxapediaEnabledOptionText = new Label();
        galaxapediaEnabledOptionText.Text = "Individual Galaxapedia Facts";
        galaxapediaEnabledOptionText.TextAlign = ContentAlignment.MiddleRight;
        galaxapediaEnabledOptionText.Anchor = AnchorStyles.Right;
        galaxapediaEnabledOptionText.Size = new Size(galaxapediaEnabledOptionText.PreferredWidth, galaxapediaEnabledOptionText.PreferredHeight);
        dynamicTableLayoutPanel.Controls.Add(galaxapediaEnabledOptionText, 0, curRow);

        galaxapediaEnabledOption = new CheckBox();
        if (VA.GetInt(">soundGroupDirList[Galaxapedia].fileCount") == 0) {
            galaxapediaEnabledOption.Enabled = false;
        } else {
            if (galaxapediaEnabled == true)
                galaxapediaEnabledOption.Checked = true;
        }
        galaxapediaEnabledOption.CheckStateChanged += new System.EventHandler(galaxapediaEnabled_CheckStateChanged);
        dynamicTableLayoutPanel.Controls.Add(galaxapediaEnabledOption, 1, curRow);
        dynamicTableLayoutPanel.SetColumnSpan(galaxapediaEnabledOption, 2);
        curRow++;


        //*** constellationsEnabled Setting ***
        bool? constellationsEnabled = VA.GetBoolean(">>constellationsEnabled");
        Label constellationsEnabledOptionText = new Label();
        constellationsEnabledOptionText.Text = "Individual Constellations Facts";
        constellationsEnabledOptionText.TextAlign = ContentAlignment.MiddleRight;
        constellationsEnabledOptionText.Anchor = AnchorStyles.Right;
        constellationsEnabledOptionText.Size = new Size(constellationsEnabledOptionText.PreferredWidth, constellationsEnabledOptionText.PreferredHeight);
        dynamicTableLayoutPanel.Controls.Add(constellationsEnabledOptionText, 0, curRow);

        constellationsEnabledOption = new CheckBox();
        if (VA.GetInt(">soundGroupDirList[Constellations].fileCount") == 0) {
            constellationsEnabledOption.Enabled = false;
        } else {
            if (constellationsEnabled == true)
                constellationsEnabledOption.Checked = true;
        }
        constellationsEnabledOption.CheckStateChanged += new System.EventHandler(constellationsEnabled_CheckStateChanged);
        dynamicTableLayoutPanel.Controls.Add(constellationsEnabledOption, 1, curRow);
        dynamicTableLayoutPanel.SetColumnSpan(constellationsEnabledOption, 2);
        curRow++;


        //*** quantumTheoryEnabled Setting ***
        bool? quantumTheoryEnabled = VA.GetBoolean(">>quantumTheoryEnabled");
        Label quantumTheoryEnabledOptionText = new Label();
        quantumTheoryEnabledOptionText.Text = "Individual Quantum Theory Facts";
        quantumTheoryEnabledOptionText.TextAlign = ContentAlignment.MiddleRight;
        quantumTheoryEnabledOptionText.Anchor = AnchorStyles.Right;
        quantumTheoryEnabledOptionText.Size = new Size(quantumTheoryEnabledOptionText.PreferredWidth, quantumTheoryEnabledOptionText.PreferredHeight);
        dynamicTableLayoutPanel.Controls.Add(quantumTheoryEnabledOptionText, 0, curRow);

        quantumTheoryEnabledOption = new CheckBox();
        if (VA.GetInt(">soundGroupDirList[Quantum Theory].fileCount") == 0) {
            quantumTheoryEnabledOption.Enabled = false;
        } else {
            if (quantumTheoryEnabled == true)
                quantumTheoryEnabledOption.Checked = true;
        }
        quantumTheoryEnabledOption.CheckStateChanged += new System.EventHandler(quantumTheoryEnabled_CheckStateChanged);
        dynamicTableLayoutPanel.Controls.Add(quantumTheoryEnabledOption, 1, curRow);
        dynamicTableLayoutPanel.SetColumnSpan(quantumTheoryEnabledOption, 2);
        curRow++;


        //*** codexEnabled Setting ***
        bool? codexEnabled = VA.GetBoolean(">>codexEnabled");
        Label codexEnabledOptionText = new Label();
        codexEnabledOptionText.Text = "Individual SF Codex Descriptions";
        codexEnabledOptionText.TextAlign = ContentAlignment.MiddleRight;
        codexEnabledOptionText.Anchor = AnchorStyles.Right;
        codexEnabledOptionText.Size = new Size(codexEnabledOptionText.PreferredWidth, codexEnabledOptionText.PreferredHeight);
        dynamicTableLayoutPanel.Controls.Add(codexEnabledOptionText, 0, curRow);

        CheckBox codexEnabledOption = new CheckBox();
        if (VA.GetInt(">>codexDescriptionsCombined.len") == 0) {
            codexEnabledOption.Enabled = false;
        } else {
            if (codexEnabled == true)
                codexEnabledOption.Checked = true;
        }
        codexEnabledOption.CheckStateChanged += new System.EventHandler(codexEnabled_CheckStateChanged);
        dynamicTableLayoutPanel.Controls.Add(codexEnabledOption, 1, curRow);
        dynamicTableLayoutPanel.SetColumnSpan(codexEnabledOption, 2);
        curRow++;


        //*** gameVoiceEnabled Setting ***
        bool? gameVoiceEnabled = VA.GetBoolean(">>gameVoiceEnabled");
        Label gameVoiceEnabledOptionText = new Label();
        gameVoiceEnabledOptionText.Text = "Game Voice Commands In Use";
        gameVoiceEnabledOptionText.TextAlign = ContentAlignment.MiddleRight;
        gameVoiceEnabledOptionText.Anchor = AnchorStyles.Right;
        gameVoiceEnabledOptionText.Size = new Size(gameVoiceEnabledOptionText.PreferredWidth, gameVoiceEnabledOptionText.PreferredHeight);
        dynamicTableLayoutPanel.Controls.Add(gameVoiceEnabledOptionText, 0, curRow);

        CheckBox gameVoiceEnabledOption = new CheckBox();
        if (gameVoiceEnabled == true)
            gameVoiceEnabledOption.Checked = true;
        gameVoiceEnabledOption.CheckStateChanged += new System.EventHandler(gameVoiceEnabled_CheckStateChanged);
        dynamicTableLayoutPanel.Controls.Add(gameVoiceEnabledOption, 1, curRow);
        dynamicTableLayoutPanel.SetColumnSpan(gameVoiceEnabledOption, 2);
        curRow++;


        //*** gameVoiceActionsQuiet Setting ***
        bool? gameVoiceActionsQuiet = VA.GetBoolean(">>gameVoiceActionsQuiet");
        Label gameVoiceActionsQuietOptionText = new Label();
        gameVoiceActionsQuietOptionText.Text = "Remain Quiet for Game Voice Commands";
        gameVoiceActionsQuietOptionText.TextAlign = ContentAlignment.MiddleRight;
        gameVoiceActionsQuietOptionText.Anchor = AnchorStyles.Right;
        gameVoiceActionsQuietOptionText.Size = new Size(gameVoiceActionsQuietOptionText.PreferredWidth, gameVoiceActionsQuietOptionText.PreferredHeight);
        dynamicTableLayoutPanel.Controls.Add(gameVoiceActionsQuietOptionText, 0, curRow);

        CheckBox gameVoiceActionsQuietOption = new CheckBox();
        if (gameVoiceActionsQuiet == true)
            gameVoiceActionsQuietOption.Checked = true;
        gameVoiceActionsQuietOption.CheckStateChanged += new System.EventHandler(gameVoiceActionsQuiet_CheckStateChanged);
        dynamicTableLayoutPanel.Controls.Add(gameVoiceActionsQuietOption, 1, curRow);
        dynamicTableLayoutPanel.SetColumnSpan(gameVoiceActionsQuietOption, 2);
        curRow++;


        //*** headphonesInUse Setting ***
        bool? headphonesInUse = VA.GetBoolean(">>headphonesInUse");
        Label headphonesInUseOptionText = new Label();
        headphonesInUseOptionText.Text = "Headphones in Use";
        headphonesInUseOptionText.TextAlign = ContentAlignment.MiddleRight;
        headphonesInUseOptionText.Anchor = AnchorStyles.Right;
        headphonesInUseOptionText.Size = new Size(headphonesInUseOptionText.PreferredWidth, headphonesInUseOptionText.PreferredHeight);
        dynamicTableLayoutPanel.Controls.Add(headphonesInUseOptionText, 0, curRow);

        CheckBox headphonesInUseOption = new CheckBox();
        if (headphonesInUse == true)
            headphonesInUseOption.Checked = true;
        headphonesInUseOption.CheckStateChanged += new System.EventHandler(headphonesInUse_CheckStateChanged);
        dynamicTableLayoutPanel.Controls.Add(headphonesInUseOption, 1, curRow);
        dynamicTableLayoutPanel.SetColumnSpan(headphonesInUseOption, 2);
        curRow += 2;






        //*** Ship Configuration Section
        Label shipConfigSectionHeading = new Label();
        shipConfigSectionHeading.Text = "Ship Configuration";
        shipConfigSectionHeading.TextAlign = ContentAlignment.MiddleLeft;
        shipConfigSectionHeading.Anchor = AnchorStyles.None;
        shipConfigSectionHeading.Font = new Font(shipConfigSectionHeading.Font.Name, shipConfigSectionHeading.Font.Size + 2.0F, shipConfigSectionHeading.Font.Style, shipConfigSectionHeading.Font.Unit);
        shipConfigSectionHeading.Size = new Size(shipConfigSectionHeading.PreferredWidth, shipConfigSectionHeading.PreferredHeight);
        shipConfigSectionHeading.Margin = new Padding(0, 10, 0, 10);
        dynamicTableLayoutPanel.Controls.Add(shipConfigSectionHeading, 0, curRow);
        dynamicTableLayoutPanel.SetColumnSpan(shipConfigSectionHeading, 3);
        curRow++;



        //*** activeShipName Setting ***
        string activeShip = VA.GetText(">>activeShipName");

        Label activeShipNameComboBoxText = new Label();
        activeShipNameComboBoxText.Text = "Active Ship";
        activeShipNameComboBoxText.Size = new Size(activeShipNameComboBoxText.PreferredWidth, activeShipNameComboBoxText.PreferredHeight);
        activeShipNameComboBoxText.TextAlign = ContentAlignment.MiddleRight;
        activeShipNameComboBoxText.Anchor = AnchorStyles.Right;
        dynamicTableLayoutPanel.Controls.Add(activeShipNameComboBoxText, 0, curRow);

        activeShipNameComboBox = new ComboBox();
        activeShipNameComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        activeShipNameComboBox.Items.AddRange(shipList.ToArray());
        if (!String.IsNullOrEmpty(activeShip) && shipList.Contains(activeShip)) {
            activeShipNameComboBox.SelectedItem = activeShip;
        }
        activeShipNameComboBox.SelectedValueChanged += new System.EventHandler(activeShipName_SelectedValueChanged);
        dynamicTableLayoutPanel.Controls.Add(activeShipNameComboBox, 1, curRow);
        dynamicTableLayoutPanel.SetColumnSpan(activeShipNameComboBox, 2);
        curRow++;



        //*** Ship Selector
        shipSelectionComboBox = new ComboBox();
        shipSelectionComboBox.Items.AddRange(shipList.ToArray());
        if (!String.IsNullOrEmpty(activeShip) && shipList.Contains(activeShip)) {
            shipSelectionComboBox.SelectedItem = activeShip;
        }
        shipSelectionComboBox.Anchor = AnchorStyles.Right;
        shipSelectionComboBox.TextChanged += new System.EventHandler(shipSelection_TextChanged);
        dynamicTableLayoutPanel.Controls.Add(shipSelectionComboBox, 0, curRow);

        editShipBtn = new Button();
        editShipBtn.Text = "Edit";
        editShipBtn.Anchor = AnchorStyles.Left;
        editShipBtn.Click += new System.EventHandler(editShipBtn_Click);
        dynamicTableLayoutPanel.Controls.Add(editShipBtn, 1, curRow);

        deleteShipBtn = new Button();
        deleteShipBtn.Text = "Delete";
        deleteShipBtn.Anchor = AnchorStyles.Left;
        deleteShipBtn.Click += new System.EventHandler(deleteShipBtn_Click);
        dynamicTableLayoutPanel.Controls.Add(deleteShipBtn, 2, curRow);
        curRow += 2;





        //*** Debug Button
        /*
        Button debugBtn = new Button();
        debugBtn.Text = "Debug";
        debugBtn.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
        debugBtn.Click += new System.EventHandler(debugBtn_Click);
        dynamicTableLayoutPanel.Controls.Add(debugBtn, 0, 13);
        */


        //*** Close Button
        Button closeBtn = new Button();
        closeBtn.Text = "Close";
        closeBtn.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
        closeBtn.Click += new System.EventHandler(closeBtn_Click);
        dynamicTableLayoutPanel.Controls.Add(closeBtn, 1, curRow);
        dynamicTableLayoutPanel.SetColumnSpan(closeBtn, 3);



        //
        // MainSettingsForm
        //
        this.AutoScaleDimensions = new SizeF(7F, 16F);
        this.AutoScaleMode = AutoScaleMode.Font;
        // this.BackColor = SystemColors.ActiveBorder;
        this.ClientSize = new Size(500, 420);
        this.Controls.Add(dynamicTableLayoutPanel);
        this.Font = new Font("Euro Caps", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
        this.MinimumSize = new Size(500, 420);
        this.Name = "MainSettingsForm";
        this.SizeGripStyle = SizeGripStyle.Hide;
        this.Text = "Configuration";
        this.Closing += new System.ComponentModel.CancelEventHandler(MainSettingsForm_Closing);
        this.ResumeLayout(false);
        // this.PerformLayout();

    }


    private void reloadProfile() {
        if (reloadProfileGuid == null)  return;
        VA.Command.Execute(reloadProfileGuid, false, false);
    }

    private bool writeSettings() {
        if (writeSettingsToFileGuid == null)  return false;
        VA.Command.Execute(writeSettingsToFileGuid, true, true);
        return true;
    }
}


public class EditShipForm : Form
{
    private VoiceAttack.VoiceAttackInvokeProxyClass VA;
    private MainSettingsForm pf;
    private string shipName;
    private string wgName;
    private Label shipHeading;
    private TableLayoutPanel dynamicTableLayoutPanel;
    private TableLayoutPanel weaponConfigLayoutPanel;
    private ComboBox weaponGroupSelectionComboBox;
    private List<ComboBox> keypressList;
    private List<ComboBox> keypressTypeList;
    private List<ComboBox> keypressTimeList;
    private string[] keyFriendlyList;
    private string[] keyList;
    private Button addWgBtn;
    private Button deleteWgBtn;
    private TextInfo ti;
    private StringComparer comparer;


    public EditShipForm(string shipName, VoiceAttack.VoiceAttackInvokeProxyClass vap, MainSettingsForm pf)
    {
        this.VA = vap;
        this.pf = pf;
        this.ti = CultureInfo.CurrentCulture.TextInfo;
        this.comparer = StringComparer.CurrentCultureIgnoreCase;
        this.shipName = ti.ToTitleCase(shipName);

        this.keypressList = new List<ComboBox>();
        this.keypressTypeList = new List<ComboBox>();
        this.keypressTimeList = new List<ComboBox>();

        keyFriendlyList = new string[]{
            "Slot 1", "Slot 2", "Slot 3", "Slot 4", "Slot 5",
            "Slot 6", "Slot 7", "Slot 8", "Slot 9", "Slot 0",
            "Primary Weapon", "Augmentation", "Propulsion Enhancer", "Open Radar",
            "Corkscrew", "Perform Action", "Fine Aiming", "Turn Left", "Turn Right",
            "Forward", "Reverse", "Pause"
        };
        keyList = new string[]{
            "Slot1", "Slot2", "Slot3", "Slot4", "Slot5",
            "Slot6", "Slot7", "Slot8", "Slot9", "Slot0",
            "PrimaryFire", "UseAugmentation", "UsePropulsionEnhancer", "Radar",
            "Corkscrew", "Action", "FineAiming", "TurnLeft", "TurnRight",
            "Accelerate", "Reverse", "Pause"
        };

        VA.SetBoolean(">>shipInfo[" + shipName + "].isInUse", true);
        if (!pf.shipList.Contains(shipName)) {
            pf.shipList.Add(shipName);
            VA.SetText(">>shipNameListStr", string.Join<string>(";", pf.shipList));
        }

        InitializeComponent();
    }



    private void EditShipForm_Closing(Object sender, System.ComponentModel.CancelEventArgs e)
    {
    }
    private void closeBtn_Click(object sender, EventArgs e)
    {
        this.Close();
    }



    //*** Source:  https://stackoverflow.com/questions/4842160/auto-width-of-comboboxs-content
    int DropDownWidth(ComboBox myCombo)
    {
        int maxWidth = 0, temp = 0;
        foreach (var obj in myCombo.Items)
        {
            temp = TextRenderer.MeasureText(obj.ToString(), myCombo.Font).Width;
            if (temp > maxWidth)
            {
                maxWidth = temp;
            }
        }
        return maxWidth;
    }

    private void weaponGroupForm_SelectedValueChanged(object sender, EventArgs e)
    {
        //*** Save all form values
        string wgDefVarName = ">>shipInfo["+shipName+"].weaponGroup["+wgName+"]";
        int i = 0, wgIdx = 0;

        foreach (ComboBox keypressCB in keypressList) {
            string keypressFriendly = keypressCB.Text;
            if (!String.IsNullOrEmpty(keypressFriendly)) {
                int commandIdx = Array.IndexOf(keyFriendlyList, keypressFriendly);
                if (commandIdx >= 0) {
                    string keypress = keyList[commandIdx];

                    if (keypressFriendly == "Pause") {
                        string pauseTime = keypressTimeList[i].Text;
                        if (String.IsNullOrEmpty(pauseTime)) {
                            pauseTime = "1";
                        }

                        VA.SetText(wgDefVarName+".weaponKeyPress[" + wgIdx + "]", "Pause " + pauseTime);

                        keypressFriendly = "Pause for " + pauseTime + " second";
                        if (pauseTime != "1")  keypressFriendly += "s";
                        VA.SetText(wgDefVarName+".weaponKeyPressFriendly[" + wgIdx + "]", keypressFriendly);

                    } else if (keypressFriendly.Substring(0, 4) == "Slot" || keypressFriendly == "Open Radar") {

                        VA.SetText(wgDefVarName+".weaponKeyPress[" + wgIdx + "]", keypress);
                        VA.SetText(wgDefVarName+".weaponKeyPressFriendly[" + wgIdx + "]", keypressFriendly);

                    } else {
                        string keypressType = keypressTypeList[i].Text;
                        if (keypressType == "Hold" || keypressType == "Release") {
                            keypress = keypressType + " " + keypress;
                            keypressFriendly = keypressType.ToLower() + " " + keypressFriendly;
                        }

                        VA.SetText(wgDefVarName+".weaponKeyPress[" + wgIdx + "]", keypress);
                        VA.SetText(wgDefVarName+".weaponKeyPressFriendly[" + wgIdx + "]", keypressFriendly);
                    }

                    wgIdx++;
                } else {
                    MessageBox.Show(keypressFriendly + " not found");
                }
            }

            i++;
        }

        VA.SetInt(wgDefVarName+".weaponKeyPress.len", wgIdx);
        VA.SetBoolean(wgDefVarName+".isActive", (wgIdx > 0));
        if (wgIdx > 0) {
            VA.SetBoolean(">>shipInfo[" + shipName + "].isInUse", true);
        }

        if (!pf.wgNameList.Contains(wgName)) {
            pf.wgNameList.Add(wgName);
        }

        pf.changesMade = true;
        pf.reloadRequired = true;
    }


    private void weaponGroupFormKeypress_SelectedValueChanged(object sender, EventArgs e)
    {
        //*** Show/Hide Form Controls, then call weaponGroupForm_SelectedValueChanged
        Control controlObj = (Control)sender;
        int controlIdx = (int)controlObj.Tag;
        string keypress = keypressList[controlIdx].Text;

        if (String.IsNullOrEmpty(keypress)
            || keypress == "Pause"
            || keypress.Substring(0, 4) == "Slot"
            || keypress == "Open Radar"
        ) {
            keypressTypeList[controlIdx].Hide();
        } else {
            keypressTypeList[controlIdx].Show();
        }
        if (keypress != "Pause") {
            keypressTimeList[controlIdx].Hide();
        } else {
            keypressTimeList[controlIdx].Show();
        }

        weaponGroupForm_SelectedValueChanged(sender, e);
    }


    private void weaponGroupSelection_SelectedValueChanged(object sender, EventArgs e)
    {
        wgName = weaponGroupSelectionComboBox.Text.Trim();
        if (!String.IsNullOrEmpty(wgName)) {
            deleteWeaponListForm();
            updateWeaponListFormDisplay();
            addWgBtn.Text = "Rename";
            deleteWgBtn.Show();
        }
    }

    private void renameShipBtn_Click(object sender, EventArgs e)
    {

        //*** Rename
        string newShipName = shipName;
        DialogResult response = ShowInputDialogBox(ref newShipName, "New ship name?", "Ship Configuration", 300, 120);
        if (response == DialogResult.Yes) {
            // Rename the weapon group

            if (newShipName.Length > 5) {
                string last4 = newShipName.Substring(newShipName.Length - 4);
                if (comparer.Compare(last4, "ship") == 0) {
                    newShipName = newShipName.Substring(0, newShipName.Length - 4);
                    newShipName = newShipName.Trim();
                }
            }
            newShipName = ti.ToTitleCase(newShipName);

            if (shipName != newShipName) {
                bool renameShip = false;

                if (pf.shipList.Contains(newShipName)) {
                    if (MessageBox.Show(newShipName + " ship already exists. Overwrite?", "Ship Configuration", MessageBoxButtons.YesNo) ==  DialogResult.Yes) {
                        renameShip = true;
                    }
                } else {
                    renameShip = true;
                }

                if (renameShip) {
                    VA.SetBoolean(">>shipInfo[" + shipName + "].isInUse", false);
                    VA.SetBoolean(">>shipInfo[" + newShipName + "].isInUse", true);

                    foreach (string wgName in pf.wgNameList) {
                        string fromVarName = ">>shipInfo[" + shipName + "].weaponGroup[" + wgName + "]";
                        string toVarName = ">>shipInfo[" + newShipName + "].weaponGroup[" + wgName + "]";

                        bool wgIsActive = false;
                        string settingName = fromVarName + ".isActive";
                        bool? boolValueN = VA.GetBoolean(settingName);
                        if (boolValueN.HasValue)  wgIsActive = boolValueN.Value;

                        VA.SetBoolean(fromVarName + ".isActive", false);
                        VA.SetBoolean(toVarName + ".isActive", wgIsActive);

                        if (wgIsActive) {
                            int wgLen = 0;
                            settingName = fromVarName + ".weaponKeyPress.len";
                            int? intValueN = VA.GetInt(settingName);
                            if (intValueN.HasValue)  wgLen = intValueN.Value;

                            VA.SetInt(fromVarName + ".weaponKeyPress.len", 0);
                            VA.SetInt(toVarName + ".weaponKeyPress.len", wgLen);

                            for (short l = 0; l < wgLen; l++) {
                                settingName = fromVarName + ".weaponKeyPress[" + l + "]";
                                string keybind = VA.GetText(settingName);
                                VA.SetText(toVarName + ".weaponKeyPress[" + l + "]", keybind);

                                settingName = fromVarName + ".weaponKeyPressFriendly[" + l + "]";
                                keybind = VA.GetText(settingName);
                                VA.SetText(toVarName + ".weaponKeyPressFriendly[" + l + "]", keybind);
                            }
                        }
                    }

                    pf.shipList.Remove(shipName);
                    if (!pf.shipList.Contains(newShipName))
                        pf.shipList.Add(newShipName);

                    //*** Export ship list settings
                    VA.SetText(">>shipNameListStr", string.Join<string>(";", pf.shipList));

                    string curSelection = pf.activeShipNameComboBox.Text;
                    pf.activeShipNameComboBox.Items.Remove(shipName);
                    pf.activeShipNameComboBox.Items.Add(newShipName);
                    if (curSelection == shipName) {
                        pf.activeShipNameComboBox.SelectedItem = newShipName;
                        VA.SetText(">>activeShipName", newShipName);
                    }

                    curSelection = pf.shipSelectionComboBox.Text;
                    pf.shipSelectionComboBox.Items.Remove(shipName);
                    pf.shipSelectionComboBox.Items.Add(newShipName);
                    if (curSelection == shipName) {
                        pf.shipSelectionComboBox.SelectedItem = newShipName;
                    }

                    shipName = newShipName;
                    shipHeading.Text = newShipName;
                    this.Text = shipName + " Ship Configuration";

                    pf.changesMade = true;
                    pf.reloadRequired = true;
                }
            }
        }
    }


    private void addWgBtn_Click(object sender, EventArgs e)
    {
        if (addWgBtn.Text == "Add") {
            //*** Add
            wgName = weaponGroupSelectionComboBox.Text.Trim();
            if (!String.IsNullOrEmpty(wgName)) {
                deleteWeaponListForm();
                updateWeaponListFormDisplay();
            }
        } else {
            //*** Rename
            string newWgName = wgName;
            DialogResult response = ShowInputDialogBox(ref newWgName, "New weapon group name?", "Weapon Group Configuration", 300, 120);
            if (response == DialogResult.Yes) {
                // Rename the weapon group
                newWgName = ti.ToTitleCase(newWgName);

                if (newWgName != wgName) {
                    bool renameWeaponGroup = false;

                    if (pf.wgNameList.Contains(newWgName)) {
                        if (MessageBox.Show("Group " + newWgName + " already exists. Overwrite?", "Ship Configuration", MessageBoxButtons.YesNo) ==  DialogResult.Yes) {
                            renameWeaponGroup = true;
                        }
                    } else {
                        renameWeaponGroup = true;
                    }

                    if (renameWeaponGroup) {
                        string fromVarName = ">>shipInfo[" + shipName + "].weaponGroup[" + wgName + "]";
                        string toVarName = ">>shipInfo[" + shipName + "].weaponGroup[" + newWgName + "]";

                        bool wgIsActive = false;
                        string settingName = fromVarName + ".isActive";
                        bool? boolValueN = VA.GetBoolean(settingName);
                        if (boolValueN.HasValue)  wgIsActive = boolValueN.Value;

                        VA.SetBoolean(fromVarName + ".isActive", false);
                        VA.SetBoolean(toVarName + ".isActive", wgIsActive);

                        if (wgIsActive) {
                            int wgLen = 0;
                            settingName = fromVarName + ".weaponKeyPress.len";
                            int? intValueN = VA.GetInt(settingName);
                            if (intValueN.HasValue)  wgLen = intValueN.Value;

                            VA.SetInt(fromVarName + ".weaponKeyPress.len", 0);
                            VA.SetInt(toVarName + ".weaponKeyPress.len", wgLen);

                            for (short l = 0; l < wgLen; l++) {
                                settingName = fromVarName + ".weaponKeyPress[" + l + "]";
                                string keybind = VA.GetText(settingName);
                                VA.SetText(toVarName + ".weaponKeyPress[" + l + "]", keybind);

                                settingName = fromVarName + ".weaponKeyPressFriendly[" + l + "]";
                                keybind = VA.GetText(settingName);
                                VA.SetText(toVarName + ".weaponKeyPressFriendly[" + l + "]", keybind);
                            }
                        }

                        if (!pf.wgNameList.Contains(newWgName)) {
                            pf.wgNameList.Add(newWgName);
                        }
                        VA.SetText(">>weaponGroupListStr", String.Join(";", pf.wgNameList));

                        weaponGroupSelectionComboBox.Items.Remove(wgName);
                        if (!weaponGroupSelectionComboBox.Items.Contains(newWgName)) {
                            weaponGroupSelectionComboBox.Items.Add(newWgName);
                        }
                        weaponGroupSelectionComboBox.SelectedItem = newWgName;

                        pf.changesMade = true;
                        pf.reloadRequired = true;
                    }
                }
            }
        }
    }

    private void weaponGroupSelection_TextChanged(object sender, EventArgs e)
    {
        string wgName = weaponGroupSelectionComboBox.Text.Trim();
        if (!pf.wgNameList.Contains(wgName)) {
            addWgBtn.Text = "Add";
            deleteWgBtn.Hide();
        } else {
            addWgBtn.Text = "Replace";
            deleteWgBtn.Show();
        }
    }
    private void deleteWgBtn_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(wgName) && pf.wgNameList.Contains(wgName)) {
            if (MessageBox.Show("Are you sure you want to delete the " + wgName + " weapon group?", "Ship Configuration", MessageBoxButtons.YesNo) ==  DialogResult.Yes) {
                string tmpVarName = ">>shipInfo[" + shipName + "].weaponGroup[" + wgName + "]";
                string settingName = tmpVarName + ".isActive";
                VA.SetBoolean(settingName, false);

                settingName = tmpVarName + ".weaponKeyPress.len";
                VA.SetInt(settingName, 0);

                weaponGroupSelectionComboBox.Text = "";
                deleteWeaponListForm();

                pf.changesMade = true;
                pf.reloadRequired = true;
            }
        }
    }

    private void addRowBtn_Click(object sender, EventArgs e)
    {
        Control controlObj = (Control)sender;
        int controlIdx = (int)controlObj.Tag;

        weaponConfigLayoutPanel.Controls.Remove(controlObj);

        addRowToWeaponListFormDisplay(controlIdx, true);

        controlObj.Tag = controlIdx + 1;
        weaponConfigLayoutPanel.RowStyles.RemoveAt(weaponConfigLayoutPanel.RowStyles.Count - 1);
        weaponConfigLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));
        weaponConfigLayoutPanel.RowCount += 1;
        weaponConfigLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 99));

        weaponConfigLayoutPanel.Controls.Add(controlObj, 0, controlIdx + 1);
        weaponConfigLayoutPanel.ScrollControlIntoView(controlObj);
    }

    private void deleteWeaponListForm() {
        if (weaponConfigLayoutPanel != null)
            dynamicTableLayoutPanel.Controls.Remove(weaponConfigLayoutPanel);

        keypressList.Clear();
        keypressTypeList.Clear();
        keypressTimeList.Clear();
    }

    public static string ToOrdinal(int num) {
        if (num <= 0) return num.ToString();

        switch (num % 100) {
            case 11:
            case 12:
            case 13:
                return num + "th";
        }

        switch(num % 10) {
            case 1:
                return num + "st";
            case 2:
                return num + "nd";
            case 3:
                return num + "rd";
            default:
                return num + "th";
        }
    }

    private void addRowToWeaponListFormDisplay(int rowNum, bool isNewItem = false)
    {
        string wgDefVarName = ">>shipInfo["+shipName+"].weaponGroup["+wgName+"]";
        int i = rowNum;
        string keyPressFriendly = "";
        if (!isNewItem) {
            keyPressFriendly = VA.GetText(wgDefVarName+".weaponKeyPressFriendly[" + i + "]");
            keyPressFriendly = ti.ToTitleCase(keyPressFriendly);
        }

        // ToOrdinal();
        Label actionRowText = new Label();
        actionRowText.Text = ToOrdinal(i + 1) + " Action";
        actionRowText.Size = new Size(actionRowText.PreferredWidth, actionRowText.PreferredHeight);
        actionRowText.TextAlign = ContentAlignment.MiddleRight;
        actionRowText.Anchor = AnchorStyles.Right;
        actionRowText.Dock = DockStyle.Fill;
        weaponConfigLayoutPanel.Controls.Add(actionRowText, 0, i);


        ComboBox keypressTypeComboBox = new ComboBox();
        keypressTypeComboBox.Name = "keypressType-" + i;
        keypressTypeComboBox.Tag = i;
        keypressTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        keypressTypeComboBox.Items.AddRange(new []{"Press", "Hold", "Release"});
        if (keyPressFriendly.Length > 5 && keyPressFriendly.Substring(0, 4) == "Hold") {
            keypressTypeComboBox.SelectedItem = "Hold";
            keyPressFriendly = keyPressFriendly.Substring(5);
        } else if (keyPressFriendly.Length > 8 && keyPressFriendly.Substring(0, 7) == "Release") {
            keypressTypeComboBox.SelectedItem = "Release";
            keyPressFriendly = keyPressFriendly.Substring(8);
        } else {
            keypressTypeComboBox.SelectedItem = "Press";
        }
        keypressTypeComboBox.SelectedValueChanged += new System.EventHandler(weaponGroupForm_SelectedValueChanged);
        keypressTypeComboBox.Width = DropDownWidth(keypressTypeComboBox) + SystemInformation.VerticalScrollBarWidth + 10;
        weaponConfigLayoutPanel.Controls.Add(keypressTypeComboBox, 1, i);

        ComboBox keypressTimeComboBox = new ComboBox();
        keypressTimeComboBox.Name = "keypressTime-" + i;
        keypressTimeComboBox.Tag = i;
        keypressTimeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        for (int idx = 1; idx <= 60; idx++)
            keypressTimeComboBox.Items.Add(idx.ToString());
        if (keyPressFriendly.Length > 6 && keyPressFriendly.Substring(0, 5) == "Pause") {
            keypressTimeComboBox.SelectedItem = keyPressFriendly.Substring(10, 2).Trim();
            keyPressFriendly = "Pause";
        }
        keypressTimeComboBox.SelectedValueChanged += new System.EventHandler(weaponGroupForm_SelectedValueChanged);
        keypressTimeComboBox.Width = DropDownWidth(keypressTimeComboBox) + SystemInformation.VerticalScrollBarWidth + 10;
        weaponConfigLayoutPanel.Controls.Add(keypressTimeComboBox, 3, i);

        ComboBox keypressComboBox = new ComboBox();
        keypressComboBox.Name = "keypress-" + i;
        keypressComboBox.Tag = i;
        keypressComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        keypressComboBox.Items.Add("");
        keypressComboBox.Items.AddRange(keyFriendlyList);
        if (!String.IsNullOrEmpty(keyPressFriendly))
            keypressComboBox.SelectedItem = keyPressFriendly;
        keypressComboBox.SelectedValueChanged += new System.EventHandler(weaponGroupFormKeypress_SelectedValueChanged);
        keypressComboBox.Width = DropDownWidth(keypressComboBox) + SystemInformation.VerticalScrollBarWidth + 10;
        weaponConfigLayoutPanel.Controls.Add(keypressComboBox, 2, i);

        if (String.IsNullOrEmpty(keyPressFriendly)
            || keyPressFriendly == "Pause"
            || keyPressFriendly.Substring(0, 4) == "Slot"
            || keyPressFriendly == "Open Radar"
        ) {
            keypressTypeComboBox.Hide();
        }
        if (keyPressFriendly != "Pause") {
            keypressTimeComboBox.Hide();
        }

        keypressTypeList.Add(keypressTypeComboBox);
        keypressList.Add(keypressComboBox);
        keypressTimeList.Add(keypressTimeComboBox);
    }


    private void updateWeaponListFormDisplay(bool addNewItem = false)
    {
        this.SuspendLayout();

        int rowsRequired, wgLen;
        int? wgLenNull;

        string wgDefVarName = ">>shipInfo["+shipName+"].weaponGroup["+wgName+"]";

        wgLenNull = VA.GetInt(wgDefVarName+".weaponKeyPress.len");
        wgLen = wgLenNull.HasValue ? wgLenNull.Value : 0;
        if (wgLen > 0) {
            rowsRequired = wgLen;
            if (addNewItem)  rowsRequired += 1;
        } else {
            rowsRequired = 1;
            addNewItem = true;
        }



        weaponConfigLayoutPanel = new TableLayoutPanel();
        weaponConfigLayoutPanel.Name = "WeaponGroupDetailLayout";
        weaponConfigLayoutPanel.TabIndex = 0;
        weaponConfigLayoutPanel.ColumnCount = 4;
        weaponConfigLayoutPanel.RowCount = rowsRequired + 2;
        // weaponConfigLayoutPanel.BorderStyle = BorderStyle.FixedSingle;
        // weaponConfigLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
        weaponConfigLayoutPanel.Size = new Size(410, 360);
        weaponConfigLayoutPanel.AutoScroll = true;
        if (weaponConfigLayoutPanel.AutoScrollMargin.Width < 5 || weaponConfigLayoutPanel.AutoScrollMargin.Height < 5) {
           weaponConfigLayoutPanel.SetAutoScrollMargin(5, 5);
        }
        weaponConfigLayoutPanel.Padding = new Padding(0, 10, SystemInformation.VerticalScrollBarWidth, 10);
        weaponConfigLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        weaponConfigLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize, 0));
        weaponConfigLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize, 0));
        weaponConfigLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize, 0));
        weaponConfigLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize, 0));
        for (int idx = 0; idx <= rowsRequired + 1; idx++) {
            weaponConfigLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));
        }
        weaponConfigLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 99));

        int i;
        for (i = 0; i < rowsRequired; i++) {
            addRowToWeaponListFormDisplay(i, i >= wgLen);
        }

        Button addRowBtn = new Button();
        addRowBtn.Text = "New Action";
        addRowBtn.Tag = i;
        addRowBtn.Anchor = AnchorStyles.Left;
        addRowBtn.Margin = new Padding(0, 0, 0, 20);
        addRowBtn.Click += new System.EventHandler(addRowBtn_Click);
        weaponConfigLayoutPanel.Controls.Add(addRowBtn, 0, i);


        dynamicTableLayoutPanel.Controls.Add(weaponConfigLayoutPanel, 0, 2);
        dynamicTableLayoutPanel.SetColumnSpan(weaponConfigLayoutPanel, 5);

        this.ResumeLayout(false);
    }


    public void debugBtn_Click(object sender, EventArgs e)
    {
        String message = "";

        string variable = VA.GetText(">>weaponGroupListStr");
        if (variable == null)  variable = "";
        List<string> fullWeaponGroupList = new List<string>(variable.Split(';'));

        //*** Static Group List
        variable = VA.GetText(">>staticGroupList");
        if (variable == null)  variable = "";
        string[] staticGroupList = variable.Split(';');

        string tmpVarName;

        message += shipName + " > isInUse: " + VA.GetBoolean(">>shipInfo[" + shipName + "].isInUse");
        message += "\n";

        if (VA.GetBoolean(">>shipInfo[" + shipName + "].isInUse") == true) {
            foreach (string wgName in fullWeaponGroupList) {
                tmpVarName = ">>shipInfo[" + shipName + "].weaponGroup[" + wgName + "]";
                if (VA.GetBoolean(tmpVarName + ".isActive") == true) {
                    int? lenN = VA.GetInt(tmpVarName + ".weaponKeyPress.len");
                    int len = lenN.HasValue ? lenN.Value : 0;

                    List<string> keyPressList = new List<string>();
                    for (short l = 0; l < len; l++) {
                        keyPressList.Add(VA.GetText(tmpVarName + ".weaponKeyPressFriendly[" + l + "]"));
                    }
                    message += shipName + " >" + wgName + " > weaponKeyPressList:  " + String.Join(", ", keyPressList);
                    message += "\n";
                }
            }

            foreach (string groupName in staticGroupList) {
                tmpVarName = ">>shipInfo[" + shipName + "].weaponGroup[" + groupName + "]";

                if (VA.GetBoolean(tmpVarName + ".isActive") == true) {
                    int? lenN = VA.GetInt(tmpVarName + ".weaponKeyPress.len");
                    int len = lenN.HasValue ? lenN.Value : 0;

                    List<string> keyPressList = new List<string>();
                    for (short l = 0; l < len; l++) {
                        keyPressList.Add(VA.GetText(tmpVarName + ".weaponKeyPressFriendly[" + l + "]"));
                    }

                    message += shipName + " >" + groupName + " > weaponKeyPressList:  " + String.Join(", ", keyPressList);
                    message += "\n";
                }
            }
        }

        MessageBox.Show(message);
    }


    // Source: https://www.codegrepper.com/code-examples/csharp/c%23+message+box+with+text+input
    private static DialogResult ShowInputDialogBox(ref string input, string prompt, string title = "Title", int width = 300, int height = 200)
    {
        Size size = new Size(width, height);
        Form inputBox = new Form();

        inputBox.FormBorderStyle = FormBorderStyle.FixedDialog;
        inputBox.ClientSize = size;
        inputBox.Text = title;

        TableLayoutPanel panel = new TableLayoutPanel();
        panel.ColumnCount = 3;
        panel.RowCount = 3;
        panel.Size = new Size(size.Width - 20, size.Height - 20);
        panel.Padding = new Padding(10);
        panel.Location = new Point(10, 10);
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 99));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize, 0));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize, 0));
        panel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));
        panel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));
        panel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));


        Label label = new Label();
        label.Text = prompt;
        panel.Controls.Add(label, 0, 0);
        panel.SetColumnSpan(label, 3);

        TextBox textBox = new TextBox();
        textBox.Text = input;
        textBox.Dock = DockStyle.Fill;
        panel.Controls.Add(textBox, 0, 1);
        panel.SetColumnSpan(textBox, 3);

        Button okButton = new Button();
        okButton.DialogResult = DialogResult.Yes;
        okButton.Name = "okButton";
        okButton.Text = "&OK";
        okButton.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
        panel.Controls.Add(okButton, 1, 2);

        Button cancelButton = new Button();
        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.Name = "cancelButton";
        cancelButton.Text = "&Cancel";
        cancelButton.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
        panel.Controls.Add(cancelButton, 2, 2);

        inputBox.AcceptButton = okButton;
        inputBox.CancelButton = cancelButton;
        inputBox.Controls.Add(panel);

        DialogResult result = inputBox.ShowDialog();
        input = textBox.Text.Trim();

        return result;
    }



    private void dynamicTableLayoutPanel_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
    {
        if (e.Row == 1)
            e.Graphics.DrawLine(Pens.Black, e.CellBounds.Location, new Point(e.CellBounds.Right, e.CellBounds.Top));
    }

    private void InitializeComponent()
    {
        this.SuspendLayout();

        dynamicTableLayoutPanel = new TableLayoutPanel();

        dynamicTableLayoutPanel.Location = new Point(10, 10);
        dynamicTableLayoutPanel.Name = "OuterLayout";
        dynamicTableLayoutPanel.Size = new Size(430, 380);
        dynamicTableLayoutPanel.ColumnCount = 5;
        dynamicTableLayoutPanel.RowCount = 4;
        dynamicTableLayoutPanel.BorderStyle = BorderStyle.FixedSingle;
        // dynamicTableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
        dynamicTableLayoutPanel.Padding = new Padding(10);
        dynamicTableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;

        dynamicTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
        dynamicTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize, 0));
        dynamicTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize, 0));
        dynamicTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize, 0));
        dynamicTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
        dynamicTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));
        dynamicTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));
        dynamicTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 99));
        dynamicTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));

        dynamicTableLayoutPanel.CellPaint += dynamicTableLayoutPanel_CellPaint;



        //*** Ship Title Section
        shipHeading = new Label();
        shipHeading.Text = shipName + " Ship";
        shipHeading.TextAlign = ContentAlignment.MiddleLeft;
        shipHeading.Anchor = AnchorStyles.None;
        shipHeading.Font = new Font(shipHeading.Font.Name, shipHeading.Font.Size + 2.0F, shipHeading.Font.Style, shipHeading.Font.Unit);
        shipHeading.Size = new Size(shipHeading.PreferredWidth, shipHeading.PreferredHeight);
        shipHeading.Margin = new Padding(0, 0, 0, 10);
        dynamicTableLayoutPanel.Controls.Add(shipHeading, 1, 0);
        dynamicTableLayoutPanel.SetColumnSpan(shipHeading, 3);

        Button renameShipBtn = new Button();
        renameShipBtn.Text = "Rename Ship";
        renameShipBtn.Anchor = AnchorStyles.Right;
        renameShipBtn.Margin = new Padding(0, 0, 0, 10);
        renameShipBtn.AutoSize = true;
        renameShipBtn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        // renameShipBtn.AutoEllipsis = false;
        renameShipBtn.Click += new System.EventHandler(renameShipBtn_Click);
        dynamicTableLayoutPanel.Controls.Add(renameShipBtn, 4, 0);


        //*** Weapon Group Selector
        weaponGroupSelectionComboBox = new ComboBox();
        weaponGroupSelectionComboBox.Items.AddRange(pf.wgNameList.ToArray());
        weaponGroupSelectionComboBox.Anchor = AnchorStyles.Right;
        weaponGroupSelectionComboBox.Margin = new Padding(0, 10, 0, 0);
        weaponGroupSelectionComboBox.TabIndex = 0;
        weaponGroupSelectionComboBox.SelectedValueChanged += new System.EventHandler(weaponGroupSelection_SelectedValueChanged);
        weaponGroupSelectionComboBox.TextChanged += new System.EventHandler(weaponGroupSelection_TextChanged);
        dynamicTableLayoutPanel.Controls.Add(weaponGroupSelectionComboBox, 1, 1);

        addWgBtn = new Button();
        addWgBtn.Text = "Add";
        addWgBtn.Anchor = AnchorStyles.Left;
        addWgBtn.Margin = new Padding(0, 10, 0, 0);
        addWgBtn.Click += new System.EventHandler(addWgBtn_Click);
        dynamicTableLayoutPanel.Controls.Add(addWgBtn, 2, 1);

        deleteWgBtn = new Button();
        deleteWgBtn.Text = "Delete";
        deleteWgBtn.Anchor = AnchorStyles.Left;
        deleteWgBtn.Margin = new Padding(0, 10, 0, 0);
        deleteWgBtn.Click += new System.EventHandler(deleteWgBtn_Click);
        dynamicTableLayoutPanel.Controls.Add(deleteWgBtn, 3, 1);
        deleteWgBtn.Hide();



        //*** Debug Button
        /*
        Button debugBtn = new Button();
        debugBtn.Text = "Debug";
        debugBtn.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
        debugBtn.Click += new System.EventHandler(debugBtn_Click);
        dynamicTableLayoutPanel.Controls.Add(debugBtn, 0, 3);
        */

        //*** Close Button
        Button closeBtn = new Button();
        closeBtn.Text = "Close";
        closeBtn.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
        closeBtn.Click += new System.EventHandler(closeBtn_Click);
        dynamicTableLayoutPanel.Controls.Add(closeBtn, 4, 3);


        //
        // EditShipForm
        //
        this.AutoScaleDimensions = new SizeF(7F, 16F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(450, 400);
        this.Controls.Add(dynamicTableLayoutPanel);
        this.Font = new Font("Euro Caps", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
        this.MinimumSize = new Size(400, 300);
        this.Name = "EditShipForm";
        this.SizeGripStyle = SizeGripStyle.Hide;
        this.Text = shipName + " Ship Configuration";
        this.Closing += new System.ComponentModel.CancelEventHandler(EditShipForm_Closing);
        this.ResumeLayout(false);
    }
}
