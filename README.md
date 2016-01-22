# HyperThreader
Let's test hyperthreading together!

If you're going to run this locally:
* Only use `Release` mode. Testing anything with debug flags enabled is silly.

Yes, I know that there's no guarantee that your threads will end up on separate CPU cores. When run locally, all threads did end up on separate cores. Work is being done to make that happen.
