# CBZCreator
A tool to create manga volumes (cbz) from folders of chapters of jpgs

You will need to get an API key from Google to use the cover downloading functionality. You can get that from https://developers.google.com/custom-search/v1/introduction

Example command which will create a volume from all the chapters in the Volume 01 folder, and fetch the cover for it (I find the ISBN from cdjapan since it has Japan only manga with ISBNs).

```
  CBZCreator.exe "D:\Downloads\workspace\Volume 01" "D:\Downloads\workspace\It's Fun Having a 300,000 Yen a Month Job...Volume 01.cbz" -isbn 9784865547726
```
