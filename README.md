## 

For prevent multiple requests to external source I use ConcurrentDictionary and because it just a proof of concept the dictionary using like a cache.

The external data retrieve in CountryTask.cs.
Why I focused on async mode with polling mechanism of result because even in the simple example of external source disallow parallel requests from the same token (for parallel is should be paid), so, I could not implement it as in commented bloc. 

Also, the polling approach works everywhere and simply scaled instead of websockets for ex..
My polling implementation is flexible, just inherit generic controller (TaskController) like this. 

You can test the implementation via Swagger /swagger/index.html

The best way to find code design solution for me is implement abstractions and interfaces. 
In the process I can dive deeply to the problem, think how they will be work together.


&nbsp;
============
&copy; [The Best Software Engineer In The Universe!](https://www.linkedin.com/in/metlinskyi/)
