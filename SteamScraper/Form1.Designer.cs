namespace SteamScraper
{
    partial class Form1
    {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.TbManualGame = new System.Windows.Forms.TextBox();
            this.BtDownload = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gbImages = new System.Windows.Forms.GroupBox();
            this.cbMarquee = new System.Windows.Forms.CheckBox();
            this.cbClearLogo = new System.Windows.Forms.CheckBox();
            this.cbBox = new System.Windows.Forms.CheckBox();
            this.cbBackground = new System.Windows.Forms.CheckBox();
            this.cbScreenshots = new System.Windows.Forms.CheckBox();
            this.cbSteamBanner = new System.Windows.Forms.CheckBox();
            this.gbVideo = new System.Windows.Forms.GroupBox();
            this.rbNoVideo = new System.Windows.Forms.RadioButton();
            this.cbVideoOnlyForGames = new System.Windows.Forms.CheckBox();
            this.rbMax = new System.Windows.Forms.RadioButton();
            this.rb480p = new System.Windows.Forms.RadioButton();
            this.gbStarRating = new System.Windows.Forms.GroupBox();
            this.rbSteamRating = new System.Windows.Forms.RadioButton();
            this.rbMetacriticScore = new System.Windows.Forms.RadioButton();
            this.rbNoStarRating = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.gbCustomFields = new System.Windows.Forms.GroupBox();
            this.cbHltb = new System.Windows.Forms.CheckBox();
            this.cbUserRatingCustomField = new System.Windows.Forms.CheckBox();
            this.cbMetacriticCustomField = new System.Windows.Forms.CheckBox();
            this.cbFlags = new System.Windows.Forms.CheckBox();
            this.cbSteamTags = new System.Windows.Forms.CheckBox();
            this.gbAdditionalApps = new System.Windows.Forms.GroupBox();
            this.cbLinkOfficialWebsite = new System.Windows.Forms.CheckBox();
            this.cbLinkPCwiki = new System.Windows.Forms.CheckBox();
            this.cbLinkSteamStore = new System.Windows.Forms.CheckBox();
            this.cbLinkSteamSpy = new System.Windows.Forms.CheckBox();
            this.cbLinkSteamDB = new System.Windows.Forms.CheckBox();
            this.btDefault = new System.Windows.Forms.Button();
            this.BtCancel = new System.Windows.Forms.Button();
            this.PlDownload = new System.Windows.Forms.Panel();
            this.LbDownload = new System.Windows.Forms.Label();
            this.gbImages.SuspendLayout();
            this.gbVideo.SuspendLayout();
            this.gbStarRating.SuspendLayout();
            this.gbCustomFields.SuspendLayout();
            this.gbAdditionalApps.SuspendLayout();
            this.PlDownload.SuspendLayout();
            this.SuspendLayout();
            // 
            // TbManualGame
            // 
            this.TbManualGame.Location = new System.Drawing.Point(247, 6);
            this.TbManualGame.Name = "TbManualGame";
            this.TbManualGame.Size = new System.Drawing.Size(302, 20);
            this.TbManualGame.TabIndex = 1;
            // 
            // BtDownload
            // 
            this.BtDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtDownload.Location = new System.Drawing.Point(212, 349);
            this.BtDownload.Name = "BtDownload";
            this.BtDownload.Size = new System.Drawing.Size(160, 30);
            this.BtDownload.TabIndex = 0;
            this.BtDownload.TabStop = false;
            this.BtDownload.Text = "Download!";
            this.BtDownload.UseVisualStyleBackColor = true;
            this.BtDownload.Click += new System.EventHandler(this.Button1_Click);
            this.BtDownload.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Button1_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Paste the entire Steam Store URL for the game";
            // 
            // gbImages
            // 
            this.gbImages.Controls.Add(this.cbMarquee);
            this.gbImages.Controls.Add(this.cbClearLogo);
            this.gbImages.Controls.Add(this.cbBox);
            this.gbImages.Controls.Add(this.cbBackground);
            this.gbImages.Controls.Add(this.cbScreenshots);
            this.gbImages.Controls.Add(this.cbSteamBanner);
            this.gbImages.Location = new System.Drawing.Point(15, 34);
            this.gbImages.Name = "gbImages";
            this.gbImages.Size = new System.Drawing.Size(181, 158);
            this.gbImages.TabIndex = 3;
            this.gbImages.TabStop = false;
            this.gbImages.Text = "Images";
            // 
            // cbMarquee
            // 
            this.cbMarquee.AutoSize = true;
            this.cbMarquee.Location = new System.Drawing.Point(6, 134);
            this.cbMarquee.Name = "cbMarquee";
            this.cbMarquee.Size = new System.Drawing.Size(119, 17);
            this.cbMarquee.TabIndex = 5;
            this.cbMarquee.Text = "Download Marquee";
            this.cbMarquee.UseVisualStyleBackColor = true;
            // 
            // cbClearLogo
            // 
            this.cbClearLogo.AutoSize = true;
            this.cbClearLogo.Location = new System.Drawing.Point(6, 111);
            this.cbClearLogo.Name = "cbClearLogo";
            this.cbClearLogo.Size = new System.Drawing.Size(128, 17);
            this.cbClearLogo.TabIndex = 4;
            this.cbClearLogo.Text = "Download Clear Logo";
            this.cbClearLogo.UseVisualStyleBackColor = true;
            // 
            // cbBox
            // 
            this.cbBox.AutoSize = true;
            this.cbBox.Location = new System.Drawing.Point(6, 88);
            this.cbBox.Name = "cbBox";
            this.cbBox.Size = new System.Drawing.Size(127, 17);
            this.cbBox.TabIndex = 3;
            this.cbBox.Text = "Download Box Image";
            this.cbBox.UseVisualStyleBackColor = true;
            // 
            // cbBackground
            // 
            this.cbBackground.AutoSize = true;
            this.cbBackground.Location = new System.Drawing.Point(6, 65);
            this.cbBackground.Name = "cbBackground";
            this.cbBackground.Size = new System.Drawing.Size(167, 17);
            this.cbBackground.TabIndex = 2;
            this.cbBackground.Text = "Download Background Image";
            this.cbBackground.UseVisualStyleBackColor = true;
            // 
            // cbScreenshots
            // 
            this.cbScreenshots.AutoSize = true;
            this.cbScreenshots.Location = new System.Drawing.Point(6, 42);
            this.cbScreenshots.Name = "cbScreenshots";
            this.cbScreenshots.Size = new System.Drawing.Size(136, 17);
            this.cbScreenshots.TabIndex = 1;
            this.cbScreenshots.Text = "Download Screenshots";
            this.cbScreenshots.UseVisualStyleBackColor = true;
            // 
            // cbSteamBanner
            // 
            this.cbSteamBanner.AutoSize = true;
            this.cbSteamBanner.Location = new System.Drawing.Point(6, 19);
            this.cbSteamBanner.Name = "cbSteamBanner";
            this.cbSteamBanner.Size = new System.Drawing.Size(144, 17);
            this.cbSteamBanner.TabIndex = 0;
            this.cbSteamBanner.Text = "Download Steam Banner";
            this.cbSteamBanner.UseVisualStyleBackColor = true;
            // 
            // gbVideo
            // 
            this.gbVideo.Controls.Add(this.rbNoVideo);
            this.gbVideo.Controls.Add(this.cbVideoOnlyForGames);
            this.gbVideo.Controls.Add(this.rbMax);
            this.gbVideo.Controls.Add(this.rb480p);
            this.gbVideo.Location = new System.Drawing.Point(212, 34);
            this.gbVideo.Name = "gbVideo";
            this.gbVideo.Size = new System.Drawing.Size(337, 64);
            this.gbVideo.TabIndex = 4;
            this.gbVideo.TabStop = false;
            this.gbVideo.Text = "Video Quality";
            // 
            // rbNoVideo
            // 
            this.rbNoVideo.AutoSize = true;
            this.rbNoVideo.Location = new System.Drawing.Point(10, 18);
            this.rbNoVideo.Name = "rbNoVideo";
            this.rbNoVideo.Size = new System.Drawing.Size(133, 17);
            this.rbNoVideo.TabIndex = 3;
            this.rbNoVideo.TabStop = true;
            this.rbNoVideo.Text = "Don\'t download videos";
            this.rbNoVideo.UseVisualStyleBackColor = true;
            this.rbNoVideo.CheckedChanged += new System.EventHandler(this.RbNoVideo_Click);
            // 
            // cbVideoOnlyForGames
            // 
            this.cbVideoOnlyForGames.AutoSize = true;
            this.cbVideoOnlyForGames.Location = new System.Drawing.Point(10, 41);
            this.cbVideoOnlyForGames.Name = "cbVideoOnlyForGames";
            this.cbVideoOnlyForGames.Size = new System.Drawing.Size(211, 17);
            this.cbVideoOnlyForGames.TabIndex = 2;
            this.cbVideoOnlyForGames.Text = "Only download videos for actual games";
            this.cbVideoOnlyForGames.UseVisualStyleBackColor = true;
            // 
            // rbMax
            // 
            this.rbMax.AutoSize = true;
            this.rbMax.Checked = true;
            this.rbMax.Location = new System.Drawing.Point(204, 18);
            this.rbMax.Name = "rbMax";
            this.rbMax.Size = new System.Drawing.Size(61, 17);
            this.rbMax.TabIndex = 1;
            this.rbMax.TabStop = true;
            this.rbMax.Text = "Highest";
            this.rbMax.UseVisualStyleBackColor = true;
            this.rbMax.CheckedChanged += new System.EventHandler(this.RbMax_Click);
            // 
            // rb480p
            // 
            this.rb480p.AutoSize = true;
            this.rb480p.Location = new System.Drawing.Point(147, 18);
            this.rb480p.Name = "rb480p";
            this.rb480p.Size = new System.Drawing.Size(49, 17);
            this.rb480p.TabIndex = 0;
            this.rb480p.Text = "480p";
            this.rb480p.UseVisualStyleBackColor = true;
            this.rb480p.CheckedChanged += new System.EventHandler(this.Rb480p_Click);
            // 
            // gbStarRating
            // 
            this.gbStarRating.Controls.Add(this.rbSteamRating);
            this.gbStarRating.Controls.Add(this.rbMetacriticScore);
            this.gbStarRating.Controls.Add(this.rbNoStarRating);
            this.gbStarRating.Controls.Add(this.label2);
            this.gbStarRating.Location = new System.Drawing.Point(212, 108);
            this.gbStarRating.Name = "gbStarRating";
            this.gbStarRating.Size = new System.Drawing.Size(337, 84);
            this.gbStarRating.TabIndex = 5;
            this.gbStarRating.TabStop = false;
            this.gbStarRating.Text = "Star Rating";
            // 
            // rbSteamRating
            // 
            this.rbSteamRating.AutoSize = true;
            this.rbSteamRating.Location = new System.Drawing.Point(6, 37);
            this.rbSteamRating.Name = "rbSteamRating";
            this.rbSteamRating.Size = new System.Drawing.Size(184, 17);
            this.rbSteamRating.TabIndex = 3;
            this.rbSteamRating.Text = "Use User Score from Steam Store";
            this.rbSteamRating.UseVisualStyleBackColor = true;
            this.rbSteamRating.CheckedChanged += new System.EventHandler(this.RbSteamRating_Click);
            // 
            // rbMetacriticScore
            // 
            this.rbMetacriticScore.AutoSize = true;
            this.rbMetacriticScore.Location = new System.Drawing.Point(211, 37);
            this.rbMetacriticScore.Name = "rbMetacriticScore";
            this.rbMetacriticScore.Size = new System.Drawing.Size(124, 17);
            this.rbMetacriticScore.TabIndex = 2;
            this.rbMetacriticScore.Text = "Use Metacritic Score";
            this.rbMetacriticScore.UseVisualStyleBackColor = true;
            this.rbMetacriticScore.CheckedChanged += new System.EventHandler(this.RbMetacriticScore_Click);
            // 
            // rbNoStarRating
            // 
            this.rbNoStarRating.AutoSize = true;
            this.rbNoStarRating.Checked = true;
            this.rbNoStarRating.Location = new System.Drawing.Point(6, 60);
            this.rbNoStarRating.Name = "rbNoStarRating";
            this.rbNoStarRating.Size = new System.Drawing.Size(329, 17);
            this.rbNoStarRating.TabIndex = 1;
            this.rbNoStarRating.TabStop = true;
            this.rbNoStarRating.Text = "Don\'t import Star Ratings (your own ratings won\'t get overwritten)";
            this.rbNoStarRating.UseVisualStyleBackColor = true;
            this.rbNoStarRating.CheckedChanged += new System.EventHandler(this.RbNoStarRating_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(318, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Warning: This will overwrite any star ratings you have set manually";
            // 
            // gbCustomFields
            // 
            this.gbCustomFields.Controls.Add(this.cbHltb);
            this.gbCustomFields.Controls.Add(this.cbUserRatingCustomField);
            this.gbCustomFields.Controls.Add(this.cbMetacriticCustomField);
            this.gbCustomFields.Controls.Add(this.cbFlags);
            this.gbCustomFields.Controls.Add(this.cbSteamTags);
            this.gbCustomFields.Location = new System.Drawing.Point(17, 204);
            this.gbCustomFields.Name = "gbCustomFields";
            this.gbCustomFields.Size = new System.Drawing.Size(179, 139);
            this.gbCustomFields.TabIndex = 6;
            this.gbCustomFields.TabStop = false;
            this.gbCustomFields.Text = "Custom Fields";
            // 
            // cbHltb
            // 
            this.cbHltb.AutoSize = true;
            this.cbHltb.Location = new System.Drawing.Point(6, 65);
            this.cbHltb.Name = "cbHltb";
            this.cbHltb.Size = new System.Drawing.Size(160, 17);
            this.cbHltb.TabIndex = 4;
            this.cbHltb.Text = "Add \'HowLongToBeat\' times";
            this.cbHltb.UseVisualStyleBackColor = true;
            // 
            // cbUserRatingCustomField
            // 
            this.cbUserRatingCustomField.AutoSize = true;
            this.cbUserRatingCustomField.Location = new System.Drawing.Point(6, 111);
            this.cbUserRatingCustomField.Name = "cbUserRatingCustomField";
            this.cbUserRatingCustomField.Size = new System.Drawing.Size(101, 17);
            this.cbUserRatingCustomField.TabIndex = 3;
            this.cbUserRatingCustomField.Text = "Add User Score";
            this.cbUserRatingCustomField.UseVisualStyleBackColor = true;
            // 
            // cbMetacriticCustomField
            // 
            this.cbMetacriticCustomField.AutoSize = true;
            this.cbMetacriticCustomField.Location = new System.Drawing.Point(6, 88);
            this.cbMetacriticCustomField.Name = "cbMetacriticCustomField";
            this.cbMetacriticCustomField.Size = new System.Drawing.Size(125, 17);
            this.cbMetacriticCustomField.TabIndex = 2;
            this.cbMetacriticCustomField.Text = "Add Metacritic Score";
            this.cbMetacriticCustomField.UseVisualStyleBackColor = true;
            // 
            // cbFlags
            // 
            this.cbFlags.AutoSize = true;
            this.cbFlags.Location = new System.Drawing.Point(6, 19);
            this.cbFlags.Name = "cbFlags";
            this.cbFlags.Size = new System.Drawing.Size(165, 17);
            this.cbFlags.TabIndex = 1;
            this.cbFlags.Text = "Add Steam Categories (Flags)";
            this.cbFlags.UseVisualStyleBackColor = true;
            // 
            // cbSteamTags
            // 
            this.cbSteamTags.AutoSize = true;
            this.cbSteamTags.Location = new System.Drawing.Point(6, 42);
            this.cbSteamTags.Name = "cbSteamTags";
            this.cbSteamTags.Size = new System.Drawing.Size(105, 17);
            this.cbSteamTags.TabIndex = 0;
            this.cbSteamTags.Text = "Add Steam Tags";
            this.cbSteamTags.UseVisualStyleBackColor = true;
            // 
            // gbAdditionalApps
            // 
            this.gbAdditionalApps.Controls.Add(this.cbLinkOfficialWebsite);
            this.gbAdditionalApps.Controls.Add(this.cbLinkPCwiki);
            this.gbAdditionalApps.Controls.Add(this.cbLinkSteamStore);
            this.gbAdditionalApps.Controls.Add(this.cbLinkSteamSpy);
            this.gbAdditionalApps.Controls.Add(this.cbLinkSteamDB);
            this.gbAdditionalApps.Location = new System.Drawing.Point(212, 204);
            this.gbAdditionalApps.Name = "gbAdditionalApps";
            this.gbAdditionalApps.Size = new System.Drawing.Size(337, 139);
            this.gbAdditionalApps.TabIndex = 7;
            this.gbAdditionalApps.TabStop = false;
            this.gbAdditionalApps.Text = "Additional Apps";
            // 
            // cbLinkOfficialWebsite
            // 
            this.cbLinkOfficialWebsite.AutoSize = true;
            this.cbLinkOfficialWebsite.Location = new System.Drawing.Point(6, 111);
            this.cbLinkOfficialWebsite.Name = "cbLinkOfficialWebsite";
            this.cbLinkOfficialWebsite.Size = new System.Drawing.Size(166, 17);
            this.cbLinkOfficialWebsite.TabIndex = 4;
            this.cbLinkOfficialWebsite.Text = "Add link to the official website";
            this.cbLinkOfficialWebsite.UseVisualStyleBackColor = true;
            // 
            // cbLinkPCwiki
            // 
            this.cbLinkPCwiki.AutoSize = true;
            this.cbLinkPCwiki.Location = new System.Drawing.Point(6, 88);
            this.cbLinkPCwiki.Name = "cbLinkPCwiki";
            this.cbLinkPCwiki.Size = new System.Drawing.Size(295, 17);
            this.cbLinkPCwiki.TabIndex = 3;
            this.cbLinkPCwiki.Text = "Add link to the PCGamingWiki article (pcgamingwiki.com)";
            this.cbLinkPCwiki.UseVisualStyleBackColor = true;
            // 
            // cbLinkSteamStore
            // 
            this.cbLinkSteamStore.AutoSize = true;
            this.cbLinkSteamStore.Location = new System.Drawing.Point(6, 65);
            this.cbLinkSteamStore.Name = "cbLinkSteamStore";
            this.cbLinkSteamStore.Size = new System.Drawing.Size(283, 17);
            this.cbLinkSteamStore.TabIndex = 2;
            this.cbLinkSteamStore.Text = "Add link to the Steam Store page (steampowered.com)";
            this.cbLinkSteamStore.UseVisualStyleBackColor = true;
            // 
            // cbLinkSteamSpy
            // 
            this.cbLinkSteamSpy.AutoSize = true;
            this.cbLinkSteamSpy.Location = new System.Drawing.Point(6, 42);
            this.cbLinkSteamSpy.Name = "cbLinkSteamSpy";
            this.cbLinkSteamSpy.Size = new System.Drawing.Size(251, 17);
            this.cbLinkSteamSpy.TabIndex = 1;
            this.cbLinkSteamSpy.Text = "Add link to the Steam Spy page (steamspy.com)";
            this.cbLinkSteamSpy.UseVisualStyleBackColor = true;
            // 
            // cbLinkSteamDB
            // 
            this.cbLinkSteamDB.AutoSize = true;
            this.cbLinkSteamDB.Location = new System.Drawing.Point(6, 19);
            this.cbLinkSteamDB.Name = "cbLinkSteamDB";
            this.cbLinkSteamDB.Size = new System.Drawing.Size(272, 17);
            this.cbLinkSteamDB.TabIndex = 0;
            this.cbLinkSteamDB.Text = "Add link to the Steam Database page (steamdb.info)";
            this.cbLinkSteamDB.UseVisualStyleBackColor = true;
            // 
            // btDefault
            // 
            this.btDefault.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btDefault.Location = new System.Drawing.Point(17, 349);
            this.btDefault.Name = "btDefault";
            this.btDefault.Size = new System.Drawing.Size(80, 30);
            this.btDefault.TabIndex = 8;
            this.btDefault.TabStop = false;
            this.btDefault.Text = "Default";
            this.btDefault.UseVisualStyleBackColor = true;
            this.btDefault.Click += new System.EventHandler(this.BtDefault_Click);
            // 
            // BtCancel
            // 
            this.BtCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtCancel.Location = new System.Drawing.Point(469, 349);
            this.BtCancel.Name = "BtCancel";
            this.BtCancel.Size = new System.Drawing.Size(80, 30);
            this.BtCancel.TabIndex = 9;
            this.BtCancel.TabStop = false;
            this.BtCancel.Text = "Cancel";
            this.BtCancel.UseVisualStyleBackColor = true;
            this.BtCancel.Click += new System.EventHandler(this.BtCancel_Click);
            // 
            // PlDownload
            // 
            this.PlDownload.Controls.Add(this.LbDownload);
            this.PlDownload.Location = new System.Drawing.Point(6, 6);
            this.PlDownload.Name = "PlDownload";
            this.PlDownload.Size = new System.Drawing.Size(556, 373);
            this.PlDownload.TabIndex = 10;
            this.PlDownload.Visible = false;
            // 
            // LbDownload
            // 
            this.LbDownload.AutoSize = true;
            this.LbDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbDownload.Location = new System.Drawing.Point(21, 162);
            this.LbDownload.Name = "LbDownload";
            this.LbDownload.Size = new System.Drawing.Size(509, 44);
            this.LbDownload.TabIndex = 0;
            this.LbDownload.Text = "Downloading... Please wait...";
            this.LbDownload.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 388);
            this.Controls.Add(this.PlDownload);
            this.Controls.Add(this.BtCancel);
            this.Controls.Add(this.btDefault);
            this.Controls.Add(this.gbAdditionalApps);
            this.Controls.Add(this.gbCustomFields);
            this.Controls.Add(this.gbStarRating);
            this.Controls.Add(this.gbVideo);
            this.Controls.Add(this.gbImages);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtDownload);
            this.Controls.Add(this.TbManualGame);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Steam Metadata Downloader";
            this.gbImages.ResumeLayout(false);
            this.gbImages.PerformLayout();
            this.gbVideo.ResumeLayout(false);
            this.gbVideo.PerformLayout();
            this.gbStarRating.ResumeLayout(false);
            this.gbStarRating.PerformLayout();
            this.gbCustomFields.ResumeLayout(false);
            this.gbCustomFields.PerformLayout();
            this.gbAdditionalApps.ResumeLayout(false);
            this.gbAdditionalApps.PerformLayout();
            this.PlDownload.ResumeLayout(false);
            this.PlDownload.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TbManualGame;
        private System.Windows.Forms.Button BtDownload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbImages;
        private System.Windows.Forms.CheckBox cbBox;
        private System.Windows.Forms.CheckBox cbBackground;
        private System.Windows.Forms.CheckBox cbScreenshots;
        private System.Windows.Forms.CheckBox cbSteamBanner;
        private System.Windows.Forms.CheckBox cbMarquee;
        private System.Windows.Forms.CheckBox cbClearLogo;
        private System.Windows.Forms.GroupBox gbVideo;
        private System.Windows.Forms.RadioButton rbMax;
        private System.Windows.Forms.RadioButton rb480p;
        private System.Windows.Forms.RadioButton rbNoVideo;
        private System.Windows.Forms.CheckBox cbVideoOnlyForGames;
        private System.Windows.Forms.GroupBox gbStarRating;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbMetacriticScore;
        private System.Windows.Forms.RadioButton rbNoStarRating;
        private System.Windows.Forms.RadioButton rbSteamRating;
        private System.Windows.Forms.GroupBox gbCustomFields;
        private System.Windows.Forms.CheckBox cbFlags;
        private System.Windows.Forms.CheckBox cbSteamTags;
        private System.Windows.Forms.CheckBox cbMetacriticCustomField;
        private System.Windows.Forms.CheckBox cbUserRatingCustomField;
        private System.Windows.Forms.CheckBox cbHltb;
        private System.Windows.Forms.GroupBox gbAdditionalApps;
        private System.Windows.Forms.CheckBox cbLinkPCwiki;
        private System.Windows.Forms.CheckBox cbLinkSteamStore;
        private System.Windows.Forms.CheckBox cbLinkSteamSpy;
        private System.Windows.Forms.CheckBox cbLinkSteamDB;
        private System.Windows.Forms.CheckBox cbLinkOfficialWebsite;
        private System.Windows.Forms.Button btDefault;
        private System.Windows.Forms.Button BtCancel;
        private System.Windows.Forms.Panel PlDownload;
        private System.Windows.Forms.Label LbDownload;
    }
}