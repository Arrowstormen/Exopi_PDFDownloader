# Exopi_PDFDownloader

1. Start the application.

2. As instructed, input the file path of the CSV-file containing the PDF links you want downloaded. 
   Example: C:\Users\User\Documents\GRI_2017_2020.csv
   
   OBS: It is expected that the CSV-file contains three columns: BRnumber/id, first link, second link. 
   Entries can be partially or fully missing without issue.

3. As instructed, input the file path of the folder where the PDFs should be stored.
   If the folder does not already exist, it will be created.
   Example: C:\Users\User\Documents\Output

4. Indicate whether you want the application to "skip" files that are already present in the folder,
   or "reset" and redownload all files, even if they are already in the folder.

5. Wait while the application attempts to download the files. This might take a while, 
   depending on the amount of files. 
   
   You should be able to see the files being written, especially in an empty folder, 
   by looking in the folder while the program is running, assuming some of the links are pointing at valid,
   available PDF files.

6. Once the application has finished downloading what it can, an overview of the results
   should be given in a CSV-file located in the same directory as the output folder.
   Example: Output_PDF_Downloader_Results.csv

   "Success" indicates that the file was downloaded successfully (or that it was skipped because it already existed).

   Blank spaces indicate that there were no data at that entry in the source CSV file.
   Alternatively, if the file was downloaded successfully from the first link, the second link is not checked (and is therefore blank).

   "Not a link to a PDF file" indicates that it was determined that the file at the given URL did not lead to a PDF file.

   "Timed out" indicates it took too long to get a response from the URL (more than 30 seconds).

   Other messages indicate what type of error / exception occurred when an attempt was made at downloading the file.
