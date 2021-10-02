Readme

#This is the readme for the miniature SEO Analyzer program developed as an exercise

#Concerns
- create the list of stop-words for the filter
- detecting the external URL
- extracting words from an URL
- the meta tag function might be slightly bugged
- URL too long unable to fully display

#Decisions
- to use either text file to store or just put the stop-words in an array
- prioritize URL first, then filter text
- how to present the results

#Guiding
1. The first page is SEO_main where the user is asked to enter either the URL or the text to be analyzed
2. The user is also required to pick at least 1 of the 4 functions. Picking 1 will affect the result of 2 and 3
3. The system will process the text/URL and return the desired results in a table format (for function 1 to 3)

#Testing
- 5 long text and 5 URL for testing
- https://github.com/WeiLiangProgramming/SEO-MVC/blob/master/testing-details

#Struggles
- meta tag function unsure whether functionable
- reduced display results as full result display seems to lead to page unable to load
