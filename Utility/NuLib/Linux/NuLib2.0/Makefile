
SUBDIRS = NuUtil NuFixUtil NuDBM NuSocket

all: install

init:
	@if [ ! -d bin ]; \
	then \
		mkdir -p bin/PIC; \
	fi
	@if [ ! -d include ]; \
	then \
		mkdir -p include; \
	fi

install: init
	@for i in $(SUBDIRS) ; \
		do cd $$i ; make install ; cd .. ;  done

clean:
	@for i in $(SUBDIRS) ; \
		do cd $$i ; make clean ; cd .. ;  done
	@rm -f bin/*.a bin/PIC/*.a include/*.h


