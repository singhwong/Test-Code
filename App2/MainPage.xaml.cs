using App2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace App2
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<PlayList> main_list;
        private void AddPlayList(string str)
        {
            PlayList playlist = new PlayList();
            playlist.PlayListName = str;
            main_list.Add(playlist);
        }

        private void Main_button_Click(object sender, RoutedEventArgs e)
        {
            AddPlayList(main_textbox.Text);
        }
        private ObservableCollection<StorageFile> allMusic;
        private ObservableCollection<Music> use_music;
        private Music the_music = new Music();
        private IRandomAccessStream main_stream;
        public MainPage()
        {
            this.InitializeComponent();
            allMusic = new ObservableCollection<StorageFile>();
            use_music = new ObservableCollection<Music>();
            main_list = new ObservableCollection<PlayList>();
            GetLocalMusic();
        }

        private async Task GetAllSongs(ObservableCollection<StorageFile> list, StorageFolder folder)

        {

            foreach (var song in await folder.GetFilesAsync())

            {

                if (song.FileType == ".mp3" || song.FileType == ".wav")

                {

                    list.Add(song);

                }

            }

            foreach (var item in await folder.GetFoldersAsync())

            {

                await GetAllSongs(list, item);

            }

        }



        private async Task ListView_Songs(ObservableCollection<StorageFile> files)

        {

            #region 设置列表歌曲信息

            foreach (var song in files)

            {

                MusicProperties song_Properties = await song.Properties.GetMusicPropertiesAsync();

                StorageItemThumbnail current_Thumb = await song.GetThumbnailAsync(

                    ThumbnailMode.MusicView,

                    300,

                    ThumbnailOptions.UseCurrentScale);

                BitmapImage album_cover = new BitmapImage();

                album_cover.SetSource(current_Thumb);

                Music music = new Music();

                music.Title = song_Properties.Title;

                music.Artist = song_Properties.Artist;

                //music.Cover = album_cover;

                main_stream = await song.OpenAsync(FileAccessMode.Read);

                //SetListTimeMethod((int)song_Properties.Duration.TotalSeconds);

                //music.MusicSeconds_Str = list_hh + ":" + list_mm + ":" + list_ss;

                music.MusicStream = main_stream;

                //music.Music_Path = song.Name;

                //music.id = num;

                music.MusicFile = song;

                //music.Music_BackGround = transParent;

                //music.album_title = song_Properties.Album;

                use_music.Add(music);

                //num++;

            }

            #endregion

        }
        private async void GetLocalMusic()
        {
            StorageFolder folder = folder = KnownFolders.MusicLibrary; ;
            await GetAllSongs(allMusic, folder);
            await ListView_Songs(allMusic);
        }

        private void ConfirmAgeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void ConfirmAgeCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void TermsOfUseContentDialog_Opened(ContentDialog sender, ContentDialogOpenedEventArgs args)
        {
            list_textbox.Text = "";
        }

        private async void ShouDialog_button_Click(object sender, RoutedEventArgs e)
        {
            ContentDialogResult result = await termsOfUseContentDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                AddPlayList(list_textbox.Text);
            }
            else
            {
            }

        }
    }
}
